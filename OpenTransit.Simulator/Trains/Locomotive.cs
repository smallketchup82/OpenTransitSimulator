// Copyright (C) 2026 smallketchup82 <ketchup@smkt.ca>.
// This file is part of OpenTransitSimulator and licensed under the LGPL-3.0-or-later license.
//
// OpenTransitSimulator is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser
// General Public License as published by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// OpenTransitSimulator is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY, without even the
// implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public
// License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along with OpenTransitSimulator.
// If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using OpenTransit.Engine.Configuration;
using OpenTransit.Engine.Graphics;
using OpenTransit.Engine.Graphics.Shapes;

namespace OpenTransit.Simulator.Trains;

public class Locomotive : Triangle
{
    // Transmission Efficiency (Diesel-Electric isn't 100% efficient)

    // Properties
    public required string Name { get; init; }

    public required string Model { get; set; }

    /// <summary>
    /// The carriages attached to this locomotive.
    /// Index 0 is the first carriage directly attached to the locomotive.
    /// </summary>
    public List<Carriage> Carriages { get; set; } = [];

    // Physics Model

    /// <summary>
    /// The mass of the locomotive in kg.
    /// * This is only the mass of the locomotive itself, not including attached carriages *
    /// </summary>
    public required double Mass { get; init; }

    /// <summary>
    /// The total mass of the locomotive and its attached carriages in kg.
    /// </summary>
    public double TotalMass { get; init; }

    /// <summary>
    /// The power of the locomotive in horsepower (hp).
    /// </summary>
    public required double Horsepower { get; init; }

    /// <summary>
    /// The maximum speed of the locomotive in km/h.
    /// Defines the upper limit for <see cref="TargetSpeed"/>.
    /// Defaults to 160 km/h if not specified.
    /// </summary>
    public double MaxSpeed { get; init; } = 160.0;

    /// <summary>
    /// The coefficient of friction between the locomotive's wheels and the rails.
    /// Used to calculate acceleration, deceleration, breaking distance, and simulate wheel slip.
    /// </summary>
    public double FrictionCoefficient { get; init; } = 0.25;

    /// <summary>
    /// If any side of the locomotive experiences a force greater than this threshold (in Newtons), it will derail.
    /// Derailment will cause the locomotive to stop moving. The locomotive cannot recover from derailment, and must be deleted.
    /// Set to <see cref="Double.PositiveInfinity"/> to disable derailment.
    /// </summary>
    public double DerailmentThreshold { get; init; } = 45.0;

    // Physics Properties

    /// <summary>
    /// The current speed of the locomotive in km/h. Negative values indicate reverse movement.
    /// To change the speed of the locomotive, set <see cref="TargetSpeed"/>
    /// </summary>
    public double Speed { get; private set; }

    /// <summary>
    /// The target speed of the locomotive in km/h. Negative values indicate reverse movement.
    /// The locomotive will accelerate or decelerate towards this speed based on its power and load, and attempt to maintain it.
    /// The value is clamped between +/- <see cref="MaxSpeed"/>.
    /// </summary>
    public double TargetSpeed {
        get;
        set => field = Math.Clamp(value, -MaxSpeed, MaxSpeed);
    }

    /// <summary>
    /// The acceleration of the locomotive in km/h².
    /// You cannot set this value directly; it is calculated based on the locomotive's power and current load.
    /// To change the speed of the locomotive, set <see cref="TargetSpeed"/>.
    /// </summary>
    public double Acceleration { get; private set; }

    public required double TransmissionEfficiency { get; init; }

    public int TestSpeed { get; init; } = 100;

    // A: Rolling Resistance (Newtons)
    // Basic friction just to move the gears/axles.
    public double DragConstantA { get; init; } = 1000.0;

    // B: Flange Friction (Newtons per m/s)
    // Increases linearly with speed.
    public double DragConstantB { get; init; } = 20.0;

    // C: Air Drag Coefficient (Newtons per (m/s)^2)
    // THE BIG ONE. Determines top speed and high-speed accel.
    public double DragConstantC { get; init; } = 5.0;

    public Locomotive() : base(50, 50)
    {
        DrawColor = Color.Blue;
        ZIndex = 1;
        Rotation = 90f;
        Anchor = Anchor.CentreLeft;
        Origin = Anchor.Centre;
    }

    private bool _debounceUpKey;
    private bool _debounceDownKey;
    private bool _debounceSpaceKey;
    private double _lastPrintTime;

    private bool _isLaunchTestActive;
    private TimeSpan _launchStartTime;
    private Vector2 _launchStartPosition;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (Keyboard.GetState().IsKeyDown(Keys.Up) && !_debounceUpKey)
        {
            _debounceUpKey = true;
            TargetSpeed += 5;
            Console.WriteLine($"Target Speed: {TargetSpeed} km/h");

            Task.Delay(100).ContinueWith(_ => _debounceUpKey = false);
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Down) && !_debounceDownKey)
        {

            _debounceDownKey = true;
            TargetSpeed -= 5;
            Console.WriteLine($"Target Speed: {TargetSpeed} km/h");

            Task.Delay(100).ContinueWith(_ => _debounceDownKey = false);
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            Rotation -= 1f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            Rotation += 1f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Space) && !_debounceSpaceKey)
        {
            TargetSpeed = TestSpeed;
            Console.WriteLine($"Target Speed: {TargetSpeed} km/h");

            // Start launch test
            _isLaunchTestActive = true;
            _launchStartTime = gameTime.TotalGameTime;
            _launchStartPosition = Position;

            _debounceSpaceKey = true;
        }

        // NOTE: Physics update logic should be done on the physics thread, not the main thread!
        CalculateAcceleration(gameTime);
        UpdatePosition(gameTime);

        if (_isLaunchTestActive && Speed >= TargetSpeed)
        {
            _isLaunchTestActive = false;

            double seconds = (gameTime.TotalGameTime - _launchStartTime).TotalSeconds;
            double distanceKm = Vector2.Distance(_launchStartPosition, Position) / Configuration.PixelsPerMeter / 1000.0;

            Console.WriteLine($"[{Name}] Launch test complete! Reached {TargetSpeed} km/h in:");
            Console.WriteLine($"- Time: {seconds:F2} seconds");
            Console.WriteLine($"- Distance: {distanceKm:F4} km");

            Task.Delay(1000).ContinueWith(_ =>
            {
                Console.WriteLine($"[{Name}] Slowing down...");
                return TargetSpeed = 0;
            });
        }
    }

    private void CalculateAcceleration(GameTime gameTime)
    {
        double dt = gameTime.ElapsedGameTime.TotalSeconds;

        if (Math.Abs(Speed - TargetSpeed) < double.Epsilon)
        {
            Speed = TargetSpeed;
            Acceleration = 0;
            return;
        }

        // Physics Constants
        const double wattsPerHp = 745.7;
        const double gravity = 9.81;
        const double kmhToMs = 1.0 / 3.6;


        // Calculate Limits
        double maxAdhesionForce = FrictionCoefficient * Mass * gravity;

        double maxBreakingForce = FrictionCoefficient * TotalMass * gravity;

        double enginePowerWatts = (Horsepower * wattsPerHp ) * TransmissionEfficiency;

        double speedMs = Speed * kmhToMs;
        double absSpeedMs = Math.Abs(speedMs);

        // Calculate Engine Force (Tractive Effort)
        // F = P / v. Avoid division by zero.
        double engineForce = absSpeedMs < 0.1
            ? maxAdhesionForce
            : enginePowerWatts / absSpeedMs;

        // Determine Direction
        // We want to accelerate towards TargetSpeed
        double speedDiff = TargetSpeed - Speed;
        int direction = Math.Sign(speedDiff);

        bool isBraking = Math.Sign(Speed) != 0 && Math.Sign(Speed) != direction;

        double resistanceForce = DragConstantA
                                 + (DragConstantB * absSpeedMs)
                                 + (DragConstantC * absSpeedMs * absSpeedMs);

        double netForce;
        if (isBraking)
        {
            // Braking: Apply max adhesion force against movement
            netForce = maxBreakingForce + resistanceForce;
        }
        else
        {
            double availablePropulsion = Math.Min(engineForce, maxAdhesionForce);
            netForce = availablePropulsion - resistanceForce;

            // If resistance exceeds propulsion, you cannot accelerate any further. This is your top speed.
            if (netForce < 0)
                netForce = 0;
        }

        // Apply direction to force
        double forceToApply = netForce * direction;

        // Calculate Acceleration (a = F/m)
        double accelerationMs2 = forceToApply / TotalMass;

        // Update State
        // Acceleration property stores rate of change in km/h per second
        Acceleration = accelerationMs2 * 3.6;

        Speed += Acceleration * dt;

        // Clamp to TargetSpeed to prevent oscillation
        if ((direction > 0 && Speed > TargetSpeed) ||
            (direction < 0 && Speed < TargetSpeed))
        {
            Speed = TargetSpeed;
            Acceleration = 0;
        }

        if (gameTime.TotalGameTime.TotalSeconds - _lastPrintTime >= 0.1)
        {
            Console.WriteLine($"--- {Name} ---");
            Console.WriteLine($"Speed: {Speed:F1} km/h");
            Console.WriteLine($"Acceleration: {Acceleration:F3} km/h/s");
            Console.WriteLine($"Target: {TargetSpeed:F0}");
            if (_isLaunchTestActive)
                Console.WriteLine("Time Elapsed: " + $"{(gameTime.TotalGameTime - _launchStartTime).TotalSeconds:F1} s");
            _lastPrintTime = gameTime.TotalGameTime.TotalSeconds;
        }
    }

    private void UpdatePosition(GameTime gameTime)
    {
        // Calculate velocity based on speed
        // Convert km/h to pixels/hour, then to pixels/second
        double metersPerHour = Speed * 1000.0;
        double pixelsPerHour = metersPerHour * Configuration.PixelsPerMeter;
        float pixelsPerSecond = (float)(pixelsPerHour / 3600.0);

        // Calculate distance to move this frame
        // Distance = Velocity * Time (Multiplying by DeltaTime, not dividing)
        float deltaTimeInSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        float distanceInPixels = pixelsPerSecond * deltaTimeInSeconds;

        // Calculate direction based on rotation (0 = North, 90 = East)
        // Adjust by -90 degrees because 0 in trigonometry is East
        float rotationInRadians = MathHelper.ToRadians(Rotation - 90);
        Vector2 direction = new Vector2((float)Math.Cos(rotationInRadians), (float)Math.Sin(rotationInRadians));

        Position += direction * distanceInPixels;
    }
}

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

using Microsoft.Xna.Framework;
using OpenTransit.Engine;
using OpenTransit.Simulator.Trains;

namespace OpenTransit.Simulator;

/// <summary>
/// The main game class for OpenTransitSimulator.
/// </summary>
public class TransitGame : EngineGame
{
    public TransitGame()
    {
        Window.Title = "Open Transit Simulator";

    }

    protected override void LoadContent()
    {
        base.LoadContent();

        // Root.AddChild(new Locomotive
        // {
        //     Name = "GO Train",
        //     Model = "basic_locomotive",
        //     Mass = 135000,
        //     TotalMass = 525000,
        //     Horsepower = 5400,
        //     MaxSpeed = 160,
        //     TransmissionEfficiency = 0.85,
        //     DrawColor = Color.Green
        // });

        Root.AddChild(new Locomotive
        {
            Name = "Shinkansen",
            Model = "basic_locomotive",
            Mass = 630000,
            TotalMass = 715000,
            Horsepower = 22900,
            TransmissionEfficiency = 0.96,
            Position = new Vector2(0, 100),
            DrawColor = Color.Gray,
            TestSpeed = 300,
            MaxSpeed = 270,
            DragConstantA = 7150.0,
            DragConstantB = 100.0,
            DragConstantC = 22.0
        });
    }
}

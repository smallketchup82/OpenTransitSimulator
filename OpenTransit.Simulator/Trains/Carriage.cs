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
using Microsoft.Xna.Framework;
using OpenTransit.Engine.Graphics;

namespace OpenTransit.Simulator.Trains;

public class Carriage : Drawable
{
    public required int MaxPassengers
    {
        get;
        init
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "MaxPassengers must be greater than zero.");

            field = value;
        }
    }

    public int CurrentPassengers { get; private set; } = 0;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    /// <summary>
    /// Updates the position of the carriage based on the locomotive and other carriages.
    /// </summary>
    private void UpdatePosition()
    {
    }
}

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
using Microsoft.Xna.Framework.Graphics;

namespace OpenTransit.Engine.Graphics.Shapes;

public class Box : Drawable
{
    private static Texture2D? _texture;

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (_texture == null)
        {
            _texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _texture.SetData([Color.White]);
        }

        spriteBatch.Draw(_texture, ScreenRect, DrawColor);
    }

    public new void Dispose()
    {
        _texture?.Dispose();
        base.Dispose();
    }
}

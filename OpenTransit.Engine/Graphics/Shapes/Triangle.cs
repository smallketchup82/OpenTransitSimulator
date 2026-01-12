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
using OpenTransit.Engine.Dependencies;

namespace OpenTransit.Engine.Graphics.Shapes;

public class Triangle : Drawable
{
    private VertexPositionColor[] _vertices = new VertexPositionColor[3];
    private BasicEffect? _effect;

    [Resolved]
    private GraphicsDevice GraphicsDevice { get; set; } = null!;

    // Constructor 1: Base and height
    public Triangle(float baseLen, float height)
    {
        Size = new Vector2(baseLen, height);

        // Isosceles triangle
        _vertices[0] = new VertexPositionColor(new Vector3(baseLen / 2, 0, 0), Color.White);
        _vertices[1] = new VertexPositionColor(new Vector3(0, height, 0), Color.White);
        _vertices[2] = new VertexPositionColor(new Vector3(baseLen, height, 0), Color.White);
    }

    // Constructor 2: Three sides
    public Triangle(float baseLen, float leftLen, float rightLen)
    {
        // Calculate coordinates
        // Using Law of Cosines derived logic or intersection of circles
        // Let P1 = (0, H), P2 = (base, H). Top vertex P3 = (x, 0).
        // H is height.
        // x = (b^2 + L^2 - R^2) / 2b

        float x = (baseLen * baseLen + leftLen * leftLen - rightLen * rightLen) / (2 * baseLen);
        float ySq = leftLen * leftLen - x * x;
        float height = (float)Math.Sqrt(Math.Abs(ySq));

        // Determine bounding box
        float minX = Math.Min(0, Math.Min(baseLen, x));
        float maxX = Math.Max(0, Math.Max(baseLen, x));
        float width = maxX - minX;

        Size = new Vector2(width, height);
        _vertices = new VertexPositionColor[3];

        // Shift vertices so they sit within [0,0] to [width, height]
        // Top vertex (x, 0) -> becomes (x - minX, 0)
        _vertices[0] = new VertexPositionColor(new Vector3(x - minX, 0, 0), Color.White);
        // Bottom left (0, h) -> becomes (0 - minX, h)
        _vertices[1] = new VertexPositionColor(new Vector3(-minX, height, 0), Color.White);
        // Bottom right (base, h) -> becomes (base - minX, h)
        _vertices[2] = new VertexPositionColor(new Vector3(baseLen - minX, height, 0), Color.White);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (_vertices == null || _vertices.Length < 3) return;

        if (_effect == null)
        {
            _effect = new BasicEffect(GraphicsDevice);
            _effect.VertexColorEnabled = true;
            _effect.View = Matrix.CreateLookAt(new Vector3(0, 0, 1), Vector3.Zero, Vector3.Up);
        }

        _effect.Projection = Matrix.CreateOrthographicOffCenter(0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, 0, 1);

        // Transform vertices
        var drawVertices = new VertexPositionColor[3];
        Vector2 originOffset = GetAnchorOffset(Origin, DrawSize);
        Vector2 pivot = DrawPosition + originOffset;

        float radians = MathHelper.ToRadians(Rotation);
        float cos = (float)Math.Cos(radians);
        float sin = (float)Math.Sin(radians);

        for (int i = 0; i < 3; i++)
        {
            Vector2 local = new Vector2(_vertices[i].Position.X, _vertices[i].Position.Y);

            // Apply Scale and Rotation around Origin
            local = (local - originOffset) * Scale;

            float rx = local.X * cos - local.Y * sin;
            float ry = local.X * sin + local.Y * cos;

            Vector2 screen = new Vector2(rx, ry) + pivot;
            drawVertices[i] = new VertexPositionColor(new Vector3(screen, 0), DrawColor);
        }

        spriteBatch.End();

        var oldRasterState = GraphicsDevice.RasterizerState;
        GraphicsDevice.RasterizerState = RasterizerState.CullNone;

        foreach (var pass in _effect.CurrentTechnique.Passes)
        {
            pass.Apply();
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, drawVertices, 0, 1);
        }

        GraphicsDevice.RasterizerState = oldRasterState;
        spriteBatch.Begin();
    }

    private Vector2 GetAnchorOffset(Anchor anchor, Vector2 size)
    {
        Vector2 offset = Vector2.Zero;
        if ((anchor & Anchor.x1) != 0) offset.X = size.X / 2f;
        else if ((anchor & Anchor.x2) != 0) offset.X = size.X;

        if ((anchor & Anchor.y1) != 0) offset.Y = size.Y / 2f;
        else if ((anchor & Anchor.y2) != 0) offset.Y = size.Y;
        return offset;
    }

    public new void Dispose()
    {
        _effect?.Dispose();
        base.Dispose();
    }
}

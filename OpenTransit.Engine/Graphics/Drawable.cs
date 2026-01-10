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

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenTransit.Engine.Graphics.Containers;

namespace OpenTransit.Engine.Graphics;

public class Drawable : IDisposable
{
    public CompositeDrawable? Parent { get; set; }

    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;
    public float Rotation { get; set; }
    public Color DrawColor { get; set; } = Color.White;

    private int _zIndex = 0;

    /// <summary>
    /// The Z-Index of the drawable
    /// </summary>
    public int ZIndex { get => _zIndex;
        set
        {
            if (_zIndex == value)
                return;

            _zIndex = value;

            Parent?.ChildOrderChanged();
        }
    }

    public Axes RelativeSizeAxes { get; set; } = Axes.None;
    public Axes RelativePositionAxes { get; set; } = Axes.None;
    public Anchor Anchor { get; set; } = Anchor.TopLeft;
    public Anchor Origin { get; set; } = Anchor.TopLeft;

    public Rectangle ScreenRect { get; private set; }
    protected Vector2 DrawPosition { get; private set; }
    protected Vector2 DrawSize { get; private set; }

    public virtual void Update(GameTime gameTime)
    {
        UpdateLayout();
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
    }

    private void UpdateLayout()
    {
        // Size
        Vector2 parentSize = Parent?.DrawSize ?? Vector2.Zero;

        float width = (RelativeSizeAxes & Axes.X) != 0 ? parentSize.X * Size.X : Size.X;
        float height = (RelativeSizeAxes & Axes.Y) != 0 ? parentSize.Y * Size.Y : Size.Y;
        DrawSize = new Vector2(width, height);

        // 2. Calculate Position (Absolute)
        // Start with the Parent's top-left
        Vector2 parentPos = Parent?.DrawPosition ?? Vector2.Zero;

        // Add the Anchor offset (Where are we pinned on the parent?)
        Vector2 anchorOffset = GetAnchorOffset(Anchor, parentSize);

        // Subtract the Origin offset (Which point of ours is pinned?)
        Vector2 originOffset = GetAnchorOffset(Origin, DrawSize);

        // Add the user's defined Position
        Vector2 posOffset = Position;
        if ((RelativePositionAxes & Axes.X) != 0) posOffset.X *= parentSize.X;
        if ((RelativePositionAxes & Axes.Y) != 0) posOffset.Y *= parentSize.Y;

        // Final Math
        DrawPosition = parentPos + anchorOffset - originOffset + posOffset;

        // Update ScreenRect (useful for input/collisions)
        ScreenRect = new Rectangle(DrawPosition.ToPoint(), DrawSize.ToPoint());
    }

    // Helper to decode your bitwise Anchor enum
    private static Vector2 GetAnchorOffset(Anchor anchor, Vector2 size)
    {
        Vector2 offset = Vector2.Zero;

        // Check Horizontal Flags
        if ((anchor & Anchor.x1) != 0)      // Centre
            offset.X = size.X / 2f;
        else if ((anchor & Anchor.x2) != 0) // Right
            offset.X = size.X;
        // else x0 (Left) is 0, so do nothing

        // Check Vertical Flags
        if ((anchor & Anchor.y1) != 0)      // Centre
            offset.Y = size.Y / 2f;
        else if ((anchor & Anchor.y2) != 0) // Bottom
            offset.Y = size.Y;
        // else y0 (Top) is 0, so do nothing

        return offset;
    }

    public void Dispose()
    {
        // Clean up unmanaged resources if needed
        GC.SuppressFinalize(this);
    }
}

/// <summary>
/// General enum to specify an "anchor" or "origin" point from the standard 9 points on a rectangle.
/// x and y counterparts can be accessed using bitwise flags.
/// </summary>
[Flags]
// todo: revisit when we have a way to exclude enum members from naming rules
[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum Anchor
{
    TopLeft = y0 | x0,
    TopCentre = y0 | x1,
    TopRight = y0 | x2,

    CentreLeft = y1 | x0,
    Centre = y1 | x1,
    CentreRight = y1 | x2,

    BottomLeft = y2 | x0,
    BottomCentre = y2 | x1,
    BottomRight = y2 | x2,

    /// <summary>
    /// The vertical counterpart is at "Top" position.
    /// </summary>
    y0 = 1,

    /// <summary>
    /// The vertical counterpart is at "Centre" position.
    /// </summary>
    y1 = 1 << 1,

    /// <summary>
    /// The vertical counterpart is at "Bottom" position.
    /// </summary>
    y2 = 1 << 2,

    /// <summary>
    /// The horizontal counterpart is at "Left" position.
    /// </summary>
    x0 = 1 << 3,

    /// <summary>
    /// The horizontal counterpart is at "Centre" position.
    /// </summary>
    x1 = 1 << 4,

    /// <summary>
    /// The horizontal counterpart is at "Right" position.
    /// </summary>
    x2 = 1 << 5,

    /// <summary>
    /// The user is manually updating the outcome, so we shouldn't.
    /// </summary>
    Custom = 1 << 6,
}

[Flags]
public enum Axes
{
    None = 0,
    X = 1,
    Y = 1 << 1,
    Both = X | Y,
}

[Flags]
public enum Edges
{
    None = 0,

    Top = 1,
    Left = 1 << 1,
    Bottom = 1 << 2,
    Right = 1 << 3,

    Horizontal = Left | Right,
    Vertical = Top | Bottom,

    All = Top | Left | Bottom | Right,
}

public enum Direction
{
    Horizontal,
    Vertical,
}

public enum RotationDirection
{
    [Description("Clockwise")] Clockwise,

    [Description("Counterclockwise")] Counterclockwise,
}

/// <summary>
/// Controls the behavior of <see cref="Drawable.RelativeSizeAxes"/> when it is set to <see cref="Axes.Both"/>.
/// </summary>
public enum FillMode
{
    /// <summary>
    /// Completely fill the parent with a relative size of 1 at the cost of stretching the aspect ratio (default).
    /// </summary>
    Stretch,

    /// <summary>
    /// Always maintains aspect ratio while filling the portion of the parent's size denoted by the relative size.
    /// A relative size of 1 results in completely filling the parent by scaling the smaller axis of the drawable to fill the parent.
    /// </summary>
    Fill,

    /// <summary>
    /// Always maintains aspect ratio while fitting into the portion of the parent's size denoted by the relative size.
    /// A relative size of 1 results in fitting exactly into the parent by scaling the larger axis of the drawable to fit into the parent.
    /// </summary>
    Fit,
}

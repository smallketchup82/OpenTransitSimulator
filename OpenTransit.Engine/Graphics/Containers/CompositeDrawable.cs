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

namespace OpenTransit.Engine.Graphics.Containers;

/// <summary>
/// A drawable that can contain other drawables.
/// Transformations applied to this drawable will also affect its children.
/// </summary>
public abstract class CompositeDrawable : Drawable
{
    private readonly List<Drawable> _internalChildren = new();

    protected IReadOnlyList<Drawable> InternalChildren => _internalChildren;

    public override void Load(DependencyContainer dependencies)
    {
        base.Load(dependencies);

        foreach (var internalChild in _internalChildren)
            internalChild.Load(Dependencies);
    }

    protected void AddInternal(Drawable child)
    {
        child.Parent = this;
        _internalChildren.Add(child);

        if (IsLoaded)
             child.Load(Dependencies);
    }

    protected bool RemoveInternal(Drawable child)
    {
        if (!_internalChildren.Remove(child)) return false;

        child.Parent = null;

        return true;
    }

    protected void ClearInternal()
    {
        foreach (Drawable child in _internalChildren)
            child.Parent = null;

        _internalChildren.Clear();
    }

    /// <summary>
    /// Updates the drawable and its children.
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        foreach (Drawable internalChild in _internalChildren)
            internalChild.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        foreach (Drawable internalChild in _internalChildren)
            internalChild.Draw(spriteBatch);
    }

    public void ChildOrderChanged()
    {
        throw new NotImplementedException();
    }
}

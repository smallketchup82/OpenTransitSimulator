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

namespace OpenTransit.Engine.Graphics.Containers;

public class Container : Container<Drawable>;

/// <summary>
/// A drawable that can contain other drawables.
/// Transformations applied to this drawable will also affect its children.
/// </summary>
public class Container<T> : CompositeDrawable where T : Drawable
{
    public IReadOnlyList<Drawable> Children
    {
        get => InternalChildren;
        set
        {
            ClearInternal();
            foreach (Drawable drawable in value)
            {
                AddInternal(drawable);
            }
        }
    }

    public void Add(Drawable child) => AddInternal(child);

    public void Remove(Drawable child) => RemoveInternal(child);

    public void Clear() => ClearInternal();
}

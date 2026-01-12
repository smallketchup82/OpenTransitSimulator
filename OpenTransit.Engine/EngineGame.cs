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
using Microsoft.Xna.Framework.Input;
using OpenTransit.Engine.Dependencies;
using OpenTransit.Engine.Graphics;
using OpenTransit.Engine.Graphics.Containers;
using OpenTransit.Engine.Graphics.Shapes;

namespace OpenTransit.Engine;

/// <summary>
/// The low level game class for OpenTransit.Engine.
/// Does not contain any drawable logic. Should be inherited by higher level game classes.
/// </summary>
public abstract class EngineGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch = null!;

    protected readonly Container Root = new();
    protected readonly DependencyContainer Dependencies = new();

    protected EngineGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Window.AllowUserResizing = true;
        Window.Title = "OpenTransit Engine";
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Dependencies.Cache(GraphicsDevice);
        Dependencies.Cache(Content);
        Dependencies.Cache(_spriteBatch);
        Dependencies.Cache(this);

        Root.Load(Dependencies);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Root.Size = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        Root.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        _spriteBatch.Begin();

        // Drawing the root container will recursively draw all its children
        // We love polymorphism
        Root.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}

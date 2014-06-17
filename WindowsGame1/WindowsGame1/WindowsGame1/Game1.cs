using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D texBird;
        Texture2D texBarn;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.IsFullScreen = false;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            rectScreen = this.GraphicsDevice.Viewport.Bounds;

            base.Initialize();
        }

        Rectangle rectScreen = Rectangle.Empty;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            texBird = Content.Load<Texture2D>("sprite1");
            texBarn = Content.Load<Texture2D>("barn");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            var gamepad = GamePad.GetState(PlayerIndex.One);
            locBird.X += gamepad.ThumbSticks.Left.X * 5.0f;
            locBird.Y -= gamepad.ThumbSticks.Left.Y * 5.0f;

            // Should I mirror my bird?
            if (gamepad.ThumbSticks.Left.X < 0)
            {
                mirrorBird = false;
            }
            else if (gamepad.ThumbSticks.Left.X > 0)
            {
                mirrorBird = true;
            }

            if (locBird.X < 0.0f) { locBird.X = 0.0f; }
            if (locBird.Y < 0.0f) { locBird.Y = 0.0f; }

            var maxX = rectScreen.Width - texBird.Width;
            var maxY = rectScreen.Height - texBird.Height;

            if (locBird.X > maxX) { locBird.X = maxX; }
            if (locBird.Y > maxY) { locBird.Y = maxY; }
            
            base.Update(gameTime);
        }

        Vector2 locBird = Vector2.Zero;
        bool mirrorBird = false;

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //spriteBatch.Draw(texBarn, Vector2.Zero, Color.White);
            spriteBatch.Draw(
                texBarn,
                rectScreen, 
                null, 
                Color.White
            );
            
            //spriteBatch.Draw(texBird, locBird, Color.White);
            spriteBatch.Draw(
                texBird,         // texture
                locBird,         // location
                null,            // source rectangle
                Color.White,     // tint
                0.0f,            // rotation
                Vector2.Zero,    // origin
                1.0f,            // scale
                mirrorBird ? SpriteEffects.FlipHorizontally : SpriteEffects.None, // flip? 
                0.0f             // depth
            );
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

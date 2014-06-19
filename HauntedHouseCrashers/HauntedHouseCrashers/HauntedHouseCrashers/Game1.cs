using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HauntedHouseCrashers
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D texSprites;
        Texture2D texFloor;

        Actor.Player playerOne;
        Actor.Player playerTwo;
        Actor.Player playerThree;
        Actor.Player playerFour;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 512;
            graphics.PreferredBackBufferWidth = 800;
            graphics.IsFullScreen = true;
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            texSprites = Content.Load<Texture2D>("HHC");
            texFloor = Content.Load<Texture2D>("Floor");
            
            string xml = string.Empty;
            using (StreamReader sr = new StreamReader(@"Content/HHC.xml"))
            {
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    xml += line;
                }
            }
            //System.Diagnostics.Debug.WriteLine(xml);
            SpriteHelper.ParseRects(xml);

            playerOne = new Actor.Player("blue", PlayerIndex.One);
            playerTwo = new Actor.Player("yellow", PlayerIndex.Two);
            playerThree = new Actor.Player("pink", PlayerIndex.Three);
            playerFour = new Actor.Player("tan", PlayerIndex.Four);
            
            playerOne.Texture =
                playerTwo.Texture =
                playerThree.Texture =
                playerFour.Texture = texSprites;

            playerOne.Location.Y = 512 - 1 * (125 / 4);
            playerTwo.Location.Y = 512 - 2 * (125 / 4);
            playerThree.Location.Y = 512 - 3 * (125 / 4);
            playerFour.Location.Y = 512 - 4 * (125 / 4);
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
            playerOne.Update(gameTime);
            playerTwo.Update(gameTime);
            playerThree.Update(gameTime);
            playerFour.Update(gameTime);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            var loc = Vector2.Zero;
            spriteBatch.Draw(texFloor, loc,null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.999999f);
            loc.X += 512 - 76; // 76 pixel overlap
            spriteBatch.Draw(texFloor, loc, null, Color.Wheat, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.999999f);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            playerOne.Draw(spriteBatch, gameTime);
            playerTwo.Draw(spriteBatch, gameTime);
            playerThree.Draw(spriteBatch, gameTime);
            playerFour.Draw(spriteBatch, gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

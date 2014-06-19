using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HauntedHouseCrashers.Actor
{
    public class Player : Actor
    {
        public Player(string colorName, PlayerIndex playerIndex) : base()
        {
            this.PlayerIndex = playerIndex;
            this.SpriteNameStanding = colorName + "/alien";
            this.SpriteNamesWalking.Add(colorName + "/alien_walk1");
            this.SpriteNamesWalking.Add(colorName + "/alien_walk2");
        }

        public PlayerIndex PlayerIndex { get; set; }
        public String SpriteNameStanding = string.Empty;
        public String SpriteNameJumpping = string.Empty;

        public List<String> SpriteNamesWalking = new List<string>();
        public int WalkFrame = 0;
        public double WalkElapsed = 0;
        public const double WALK_DELAY = 0.2;

        public bool IsWalking = false;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var gamepad = GamePad.GetState(this.PlayerIndex);
            MoveActor(gamepad.ThumbSticks.Left * 3.0f);
            IsWalking = (gamepad.ThumbSticks.Left != Vector2.Zero);

            if (IsWalking)
            {
                WalkElapsed -= gameTime.ElapsedGameTime.TotalSeconds;
                if (WalkElapsed < 0)
                {
                    WalkFrame = (WalkFrame + 1) % SpriteNamesWalking.Count;
                    WalkElapsed = WALK_DELAY;
                }
            }
        }

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            Vector2 origin = new Vector2(
                SpriteHelper.SpriteRects["shadow"].Width / 2,
                SpriteHelper.SpriteRects["shadow"].Height);

            float minY = 512 - 125;
            float maxY = 512;
            float depth = MathHelper.Clamp(1.0f - (Location.Y - minY + 1) / (maxY - minY), 0.0001f, 1.0f);
            
            batch.Draw(
                Texture,            // texture
                Location + new Vector2(0, 10),           // location
                SpriteHelper.SpriteRects["shadow"], // Source Rectangle
                Color.White,        // tint
                0.0f,               // rotation
                origin,             // origin
                1.0f,               // scale
                SpriteEffects.None, // flip?
                depth + 0.00001f              // depth
            );

            if (IsWalking)
            {
                origin = new Vector2(
                    SpriteHelper.SpriteRects[SpriteNamesWalking[WalkFrame]].Width / 2,
                    SpriteHelper.SpriteRects[SpriteNamesWalking[WalkFrame]].Height);
                batch.Draw(
                    Texture,            // texture
                    Location,           // location
                    SpriteHelper.SpriteRects[SpriteNamesWalking[WalkFrame]], // Source Rectangle
                    Color.White,        // tint
                    0.0f,               // rotation
                    origin,             // origin
                    1.0f,               // scale
                    SpriteEffects.None, // flip?
                    depth                // depth
                );
            }
            else
            {
                origin = new Vector2(
                    SpriteHelper.SpriteRects[SpriteNameStanding].Width / 2,
                    SpriteHelper.SpriteRects[SpriteNameStanding].Height);
                batch.Draw(
                    Texture,            // texture
                    Location,           // location
                    SpriteHelper.SpriteRects[SpriteNameStanding], // Source Rectangle
                    Color.White,        // tint
                    0.0f,               // rotation
                    origin,             // origin
                    1.0f,               // scale
                    SpriteEffects.None, // flip?
                    depth                // depth
                );
            }
        }

        public override void MoveActor(Vector2 delta)
        {
            base.MoveActor(delta);

            float maxX = 800;
            float minY = 512 - 125;
            float maxY = 512;

            float floorHeight = maxY - minY;
            float fromFloorBottom = maxY - Location.Y;
            float minX = 26.0f + 56.0f * (fromFloorBottom / floorHeight);

            if (Location.X < minX) { Location.X = minX; }
            if (Location.X > maxX) { Location.X = maxX; }
            if (Location.Y > maxY) { Location.Y = maxY; }
            if (Location.Y < minY) { Location.Y = minY; }
        }

    }
}

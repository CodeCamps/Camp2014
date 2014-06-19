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
            this.HasShadow = true;
            this.PlayerIndex = playerIndex;
            this.SpriteNameStanding = colorName + "/alien";
            this.SpriteNamesWalking.Add(colorName + "/alien_walk1");
            this.SpriteNamesWalking.Add(colorName + "/alien_walk2");
            this.SpriteNameJumpping = colorName + "/alien_jump";
        }

        public int VerticalLocation = 0;

        public PlayerIndex PlayerIndex { get; set; }
        public String SpriteNameStanding = string.Empty;

        public String SpriteNameJumpping = string.Empty;
        public double JumpElapsed = 0;
        public double JUMP_DURATION = 1.0;
        public float JUMP_HEIGHT = 75;

        public List<String> SpriteNamesWalking = new List<string>();
        public int WalkFrame = 0;
        public double WalkElapsed = 0;
        public const double WALK_DELAY = 0.2;

        public bool IsWalking = false;
        public bool IsJumpping = false;
        public bool IsMirrored = false;

        public GamePadState gamepadPrev;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var gamepad = GamePad.GetState(this.PlayerIndex);
            MoveActor(gamepad.ThumbSticks.Left * 3.0f);
            IsWalking = (gamepad.ThumbSticks.Left != Vector2.Zero);
            IsJumpping = (JumpElapsed > 0);
            var doJump = (
                !IsJumpping &&
                gamepad.Buttons.A == ButtonState.Pressed &&
                gamepadPrev.Buttons.A != ButtonState.Pressed
            );

            gamepadPrev = gamepad;

            if (doJump)
            {
                JumpElapsed = JUMP_DURATION;
            }

            if (gamepad.ThumbSticks.Left.X > 0)
            {
                IsMirrored = false;
            }
            else if (gamepad.ThumbSticks.Left.X < 0)
            {
                IsMirrored = true;
            }

            VerticalLocation = 0;

            if (IsJumpping)
            {
                VerticalLocation = (int)Math.Round(Math.Sin(JumpElapsed * Math.PI) * JUMP_HEIGHT);
                JumpElapsed -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (IsWalking)
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
            base.Draw(batch, gameTime);
            Vector2 origin;

            float minY = 512 - 125;
            float maxY = 512;
            float depth = MathHelper.Clamp(1.0f - (Location.Y - minY + 1) / (maxY - minY), 0.0001f, 1.0f);

            if (IsJumpping)
            {
                origin = new Vector2(
                    SpriteHelper.SpriteRects[SpriteNamesWalking[WalkFrame]].Width / 2,
                    SpriteHelper.SpriteRects[SpriteNamesWalking[WalkFrame]].Height);
                var loc = Location;
                loc.Y -= VerticalLocation;
                batch.Draw(
                    Texture,            // texture
                    loc,           // location
                    SpriteHelper.SpriteRects[SpriteNameJumpping], // Source Rectangle
                    Color.White,        // tint
                    0.0f,               // rotation
                    origin,             // origin
                    1.0f,               // scale
                    IsMirrored ? SpriteEffects.FlipHorizontally : SpriteEffects.None, // flip?
                    depth                // depth
                );
            }
            else if (IsWalking)
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
                    IsMirrored ? SpriteEffects.FlipHorizontally : SpriteEffects.None, // flip?
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
                    IsMirrored ? SpriteEffects.FlipHorizontally : SpriteEffects.None, // flip?
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

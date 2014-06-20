using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HauntedHouseCrashers.Actor
{
    public class Character : Actor
    {
        public String SpriteNameStanding = string.Empty;
        public String SpriteNameJumping = string.Empty;
        public List<String> SpriteNamesWalking = new List<string>();

        public int WalkFrame = 0;
        public double WalkElapsed = 0;
        public double WALK_DELAY = 0.2;

        public bool IsWalking = false;
        public bool IsJumping = false;
        public bool IsMirrored = false;

        public int VerticalLocation = 0;

        public double JumpElapsed = 0;
        public double JUMP_DURATION = 1.0;
        public float JUMP_HEIGHT = 75;

        public Character()
            : base()
        {
            this.HasShadow = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsJumping)
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

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            Vector2 origin;

            float minY = 512 - 125;
            float maxY = 512;
            float depth = MathHelper.Clamp(1.0f - (Location.Y - minY + 1) / (maxY - minY), 0.0001f, 1.0f);

            if (IsJumping)
            {
                origin = new Vector2(
                    SpriteHelper.SpriteRects[SpriteNameJumping].Width / 2,
                    SpriteHelper.SpriteRects[SpriteNameJumping].Height);
                var loc = Location;
                loc.Y -= VerticalLocation;
                batch.Draw(
                    Texture,            // texture
                    loc,           // location
                    SpriteHelper.SpriteRects[SpriteNameJumping], // Source Rectangle
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
                var loc = Location;
                loc.Y -= VerticalLocation;
                batch.Draw(
                    Texture,            // texture
                    loc,           // location
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
            base.Draw(batch, gameTime);
        }
    }
}

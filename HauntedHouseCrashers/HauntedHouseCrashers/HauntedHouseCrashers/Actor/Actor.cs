using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HauntedHouseCrashers.Actor
{
    public class Actor
    {
        protected static Random _rand = new Random();

        public Vector2 Location = Vector2.Zero;
        public int Health = 1;
        public Texture2D Texture = null;
        public List<Texture2D> SpriteNames = new List<Texture2D>();
        public bool HasShadow = false;
        public bool ReadyToRemove = false;
        public Rectangle Bounds { get; set; }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch batch, GameTime gameTime)
        {
            if (Screens.GameScreen.IsDebug)
            {
                var trans = new Color(1.0f, 0.0f, 0.0f, 0.25f);
                batch.Draw(Texture, this.Bounds, SpriteHelper.SpriteRects["selectAlien"], trans);
            }

            if (HasShadow)
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

            }
        }

        public virtual void MoveActor(Vector2 delta)
        {
            Location.X += delta.X;
            Location.Y -= delta.Y;
        }
    }
}

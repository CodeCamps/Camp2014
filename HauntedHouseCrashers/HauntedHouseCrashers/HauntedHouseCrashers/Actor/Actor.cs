using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HauntedHouseCrashers.Actor
{
    public class Actor
    {
        public Vector2 Location = Vector2.Zero;
        public int Health = 0;
        public Texture2D Texture = null;

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch batch, GameTime gameTime)
        {
        }

        public virtual void MoveActor(Vector2 delta)
        {
            Location.X += delta.X;
            Location.Y -= delta.Y;
        }
    }
}

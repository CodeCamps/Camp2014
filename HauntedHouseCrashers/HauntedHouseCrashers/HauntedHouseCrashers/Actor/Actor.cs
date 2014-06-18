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
            var origin = new Vector2(Texture.Width / 2, Texture.Height);
            batch.Draw(
                Texture,            // texture
                Location,           // location
                null,               // Source Rectangle
                Color.White,        // tint
                0.0f,               // rotation
                origin,             // origin
                1.0f,               // scale
                SpriteEffects.None, // flip?
                0.0f                // depth
            );
        }

        public virtual void MoveActor(Vector2 delta)
        {
            Location.X += delta.X;
            Location.Y -= delta.Y;

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

            System.Diagnostics.Debug.WriteLine(Location);
        }
    }
}

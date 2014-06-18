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
            batch.Draw(Texture, Location, Color.White);
        }

        public virtual void MoveActor(Vector2 delta)
        {
            Location.X += delta.X;
            Location.Y -= delta.Y;

            float maxX = 800 - Texture.Width;
            float minY = 512 - Texture.Height - 125;
            float maxY = 512 - Texture.Height;

            float floorHeight = maxY - minY;
            float fromFloorBottom = maxY - Location.Y;
            float minX = 60.0f * (fromFloorBottom / floorHeight);

            if (Location.X < minX) { Location.X = minX; }
            if (Location.X > maxX) { Location.X = maxX; }
            if (Location.Y > maxY) { Location.Y = maxY; }
            if (Location.Y < minY) { Location.Y = minY; }

            System.Diagnostics.Debug.WriteLine(Location);
        }
    }
}

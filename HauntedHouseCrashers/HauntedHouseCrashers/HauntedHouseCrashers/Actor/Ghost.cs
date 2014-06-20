using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HauntedHouseCrashers.Actor
{
    public class Ghost : Character
    {
        public Vector2 Movement = Vector2.Zero;
        public Ghost()
            : base()
        {
            HasShadow = false;
            SpriteNameStanding = "ghost";
            SpriteNamesWalking.Add("ghost");
            IsWalking = true;
            Movement = new Vector2(0, -2);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Location += Movement;
            if (Location.Y < -200)
            {
                ReadyToRemove = true;
            }
        }
    }
}

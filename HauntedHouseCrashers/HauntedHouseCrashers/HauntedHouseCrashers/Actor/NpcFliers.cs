using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HauntedHouseCrashers.Actor
{
    public class NpcFliers : Character
    {
        public double randRate = 0;
        public Vector2 Movement = new Vector2(-0.8f, 0);

        public NpcFliers() : base()
        {
            this.randRate = _rand.NextDouble();
            this.WALK_DELAY = 0.1;
            this.WalkFrame = _rand.Next(2);
            this.IsWalking = true;
            this.SpriteNameStanding = string.Empty;
            this.SpriteNameJumping = string.Empty;
        }

        public NpcFliers(double walkDelay, string[] walkFrames) : this()
        {
            this.WALK_DELAY = walkDelay;
            foreach (string frame in walkFrames)
            {
                this.SpriteNamesWalking.Add(frame);
            }
        }

        public NpcFliers(string[] walkFrames) : this()
        {
            foreach (string frame in walkFrames)
            {
                this.SpriteNamesWalking.Add(frame);
            }
        }

        public override void Update(GameTime gameTime)
        {
            Location += Movement;
            if (Location.X < -100)
            {
                ReadyToRemove = true;
            }

            VerticalLocation = (int)(75.0 + 50.0 * Math.Sin(gameTime.TotalGameTime.TotalSeconds * (randRate + 0.5)));
            base.Update(gameTime);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HauntedHouseCrashers.Actor
{
    public class NpcWalkers : NpcFliers
    {
        public NpcWalkers()
            : base() { this.IsWalker = true; }

        public NpcWalkers(double walkDelay, string[] walkFrames)
            : base(walkDelay, walkFrames) { this.IsWalker = true; }

        public NpcWalkers(string[] walkFrames)
            : base(walkFrames) { this.IsWalker = true; }

        //public override void Update(GameTime gameTime)
        //{
        //    base.Update(gameTime);
        //    VerticalLocation = 0;
        //}

        public override void Update(GameTime gameTime)
        {
            Location += Movement;
            if (Location.X < -100)
            {
                ReadyToRemove = true;
            }

            VerticalLocation = 0;
            base.Update(gameTime);
            VerticalLocation = 0;
        }
    }
}

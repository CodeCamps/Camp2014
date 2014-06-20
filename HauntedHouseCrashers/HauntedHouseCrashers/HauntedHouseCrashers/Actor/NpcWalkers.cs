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
            : base() {}

        public NpcWalkers(double walkDelay, string[] walkFrames) 
            : base(walkDelay, walkFrames) {}

        public NpcWalkers(string[] walkFrames) 
            : base(walkFrames) {}

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            VerticalLocation = 0;
        }
    }
}

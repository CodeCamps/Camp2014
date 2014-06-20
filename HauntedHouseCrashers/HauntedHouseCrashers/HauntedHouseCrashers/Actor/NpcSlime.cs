using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HauntedHouseCrashers.Actor
{
    public class NpcSlime : NpcWalkers
    {
        public NpcSlime()
            : base(new string[] { "slimeGreenWalk1", "slimeGreenWalk2" })
        {
            this.Movement.X = -0.4f;

            int rand = _rand.Next(3);
            if (rand == 1)
            {
                SpriteNamesWalking.Clear();
                SpriteNamesWalking.Add("slimeBlueWalk1");
                SpriteNamesWalking.Add("slimeBlueWalk2");
            }
            else if (rand == 2)
            {
                SpriteNamesWalking.Clear();
                SpriteNamesWalking.Add("slimePinkWalk1");
                SpriteNamesWalking.Add("slimePinkWalk2");
            }
        }
    }
}

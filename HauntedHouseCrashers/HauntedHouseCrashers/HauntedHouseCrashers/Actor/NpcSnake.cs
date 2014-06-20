using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HauntedHouseCrashers.Actor
{
    public class NpcSnake : NpcWalkers
    {
        public NpcSnake()
            : base(new string[] { "snakeWalk1", "snakeWalk2" })
        {
            int rand = _rand.Next(2);
            if (rand == 1)
            {
                SpriteNamesWalking.Clear();
                SpriteNamesWalking.Add("wormWalk1");
                SpriteNamesWalking.Add("wormWalk2");
            }
        }
    }
}

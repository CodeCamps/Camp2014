using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HauntedHouseCrashers.Actor
{
    public class NpcSpider : NpcWalkers
    {
        public NpcSpider() : base(new string[] { "spiderWalk1", "spiderWalk2" }) { }
    }
}

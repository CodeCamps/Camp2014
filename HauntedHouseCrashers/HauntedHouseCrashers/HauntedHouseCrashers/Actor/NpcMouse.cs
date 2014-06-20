using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HauntedHouseCrashers.Actor
{
    public class NpcMouse : NpcWalkers
    {
        public NpcMouse() : base(new string[] { "mouseWalk1", "mouseWalk2" }) { }
    }
}

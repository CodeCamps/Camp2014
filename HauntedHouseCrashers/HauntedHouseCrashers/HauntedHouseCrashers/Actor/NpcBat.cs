using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HauntedHouseCrashers.Actor
{
    public class NpcBat : NpcFliers
    {
        public NpcBat() : base(new string[] {"batFly1", "batFly2"}) 
        {
            this.Health = 3;
            this.Movement.X = -1.0f;
        }
    }
}

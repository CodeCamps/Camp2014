using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HauntedHouseCrashers.Actor
{
    public class NpcFly : NpcFliers
    {
        public NpcFly() : base(new string[] {"flyFly1", "flyFly2"}) 
        {
        }
    }
}

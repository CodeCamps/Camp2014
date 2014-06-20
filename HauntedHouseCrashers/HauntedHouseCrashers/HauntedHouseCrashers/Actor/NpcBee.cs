using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HauntedHouseCrashers.Actor
{
    public class NpcBee : NpcFliers
    {
        public NpcBee() : base(new string[] {"beeFly1", "beeFly2"}) 
        {
        }

    }
}

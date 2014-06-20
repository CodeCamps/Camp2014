using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HauntedHouseCrashers.Screens
{
    public class Screen
    {
        //public static Texture2D Sprites { get; set; }
        //public static Texture2D Sprites2 { get; set; }
        //public static Texture2D Sprites3 { get; set; }

        public virtual void LoadContent(ContentManager content)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch batch, GameTime gameTime)
        {
        }

        public virtual void ShowingScreen()
        {
        }

        public virtual void UnloadingScreen()
        {
        }
    }
}

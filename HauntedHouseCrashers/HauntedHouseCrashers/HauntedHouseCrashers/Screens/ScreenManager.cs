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
    public class ScreenManager
    {
        private static Screen CurrentScreen { get; set; }
        public static ContentManager Content { get; set; }

        public static void ShowScreen(Screen screen)
        {
            if (CurrentScreen != null)
            {
                CurrentScreen.UnloadingScreen();
            }
            CurrentScreen = screen;
            CurrentScreen.ShowingScreen();
            CurrentScreen.LoadContent(Content);
        }

        public static void LoadContent(ContentManager content)
        {
            if (CurrentScreen != null)
            {
                CurrentScreen.LoadContent(content ?? Content);
            }
        }

        public static void Update(GameTime gameTime)
        {
            if (CurrentScreen != null)
            {
                CurrentScreen.Update(gameTime);
            }
        }

        public static void Draw(SpriteBatch batch, GameTime gameTime)
        {
            if (CurrentScreen != null)
            {
                CurrentScreen.Draw(batch, gameTime);
            }
        }
    }
}

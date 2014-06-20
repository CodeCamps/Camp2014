using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HauntedHouseCrashers.Screens
{
    public class GameScreen : Screen
    {
        Texture2D texSprites;
        Texture2D texFloor;

        List<Actor.Player> Players = new List<Actor.Player>();
        List<Actor.NpcBee> Bees = new List<Actor.NpcBee>();

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            texSprites = content.Load<Texture2D>("HHC");
            texFloor = content.Load<Texture2D>("Floor");

            string xml = string.Empty;
            using (StreamReader sr = new StreamReader(@"Content/HHC.xml"))
            {
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    xml += line;
                }
            }

            SpriteHelper.ParseRects(xml);
        }

        public override void Update(GameTime gameTime)
        {
            if (Players.Count == 0)
            {
                // init players
                Actor.Player player;
                if (GamePad.GetState(PlayerIndex.One).IsConnected)
                {
                    player = new Actor.Player("blue", PlayerIndex.One);
                    player.Texture = texSprites;
                    player.Location.Y = 512 - 3 * (125 / 4);
                    Players.Add(player);
                }
                if (GamePad.GetState(PlayerIndex.Two).IsConnected)
                {
                    player = new Actor.Player("yellow", PlayerIndex.Two);
                    player.Texture = texSprites;
                    player.Location.Y = 512 - 2 * (125 / 4);
                    Players.Add(player);
                }
                if (GamePad.GetState(PlayerIndex.Three).IsConnected)
                {
                    player = new Actor.Player("pink", PlayerIndex.Three);
                    player.Texture = texSprites;
                    player.Location.Y = 512 - 4 * (125 / 4);
                    Players.Add(player);
                }
                if (GamePad.GetState(PlayerIndex.Four).IsConnected)
                {
                    player = new Actor.Player("green", PlayerIndex.Four);
                    player.Texture = texSprites;
                    player.Location.Y = 512 - 1 * (125 / 4);
                    Players.Add(player);
                }

                var bee = new Actor.NpcBee();
                bee.Location = new Vector2(800 - 90, 512 - 80);
                bee.Texture = texSprites;
                Bees.Add(bee);
                
                bee = new Actor.NpcBee();
                bee.Location = new Vector2(800 - 50, 512 - 40);
                bee.Texture = texSprites;
                Bees.Add(bee);
                
                bee = new Actor.NpcBee();
                bee.Location = new Vector2(800 - 130, 512 - 120);
                bee.Texture = texSprites;
                Bees.Add(bee);
            }

            foreach (var player in Players)
            {
                player.Update(gameTime);
            }

            foreach (var bee in Bees)
            {
                bee.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            batch.Begin();
            var loc = Vector2.Zero;
            loc.X -= 512 - 76; // 76 pixel overlap
            batch.Draw(texFloor, loc, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.999999f);
            loc.X += 512 - 76; // 76 pixel overlap
            batch.Draw(texFloor, loc, null, Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.999999f);
            loc.X += 512 - 76; // 76 pixel overlap
            batch.Draw(texFloor, loc, null, Color.Gray, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.999999f);
            batch.End();

            batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            foreach (var player in Players)
            {
                player.Draw(batch, gameTime);
            }
            
            foreach (var bee in Bees)
            {
                bee.Draw(batch, gameTime);
            }
            
            batch.End();

            base.Draw(batch, gameTime);
        }
    }
}

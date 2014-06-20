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

        public static bool IsDebug = false;

        List<Actor.Player> Players = new List<Actor.Player>();
        public static List<Actor.Actor> Enemies = new List<Actor.Actor>();

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
                Enemies.Add(bee);
                
                var fly = new Actor.NpcFly();
                fly.Location = new Vector2(800 - 50, 512 - 40);
                fly.Texture = texSprites;
                Enemies.Add(fly);
                
                var bat = new Actor.NpcBat();
                bat.Location = new Vector2(800 - 130, 512 - 120);
                bat.Texture = texSprites;
                Enemies.Add(bat);
            }

            foreach (var player in Players)
            {
                player.Update(gameTime);
            }

            foreach (var obj in Enemies)
            {
                obj.Update(gameTime);
            }

            for (int i = 0; i < Enemies.Count; )
            {
                if (Enemies[i].ReadyToRemove)
                {
                    Enemies.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            var enemyCount = 0;
            foreach (var enemy in Enemies)
            {
                if (enemy is Actor.Fireball)
                {
                    // ignore
                }
                else
                {
                    enemyCount++;
                }
            }

            if (enemyCount < 1)
            {
                CreateNewWave();
            }

            base.Update(gameTime);
        }

        public void CreateNewWave()
        {
            var bee = new Actor.NpcBee();
            bee.Location = new Vector2(800 + 20, 512 - 80);
            bee.Texture = texSprites;
            Enemies.Add(bee);

            var fly = new Actor.NpcFly();
            fly.Location = new Vector2(800 + 40, 512 - 40);
            fly.Texture = texSprites;
            Enemies.Add(fly);

            var bat = new Actor.NpcBat();
            bat.Location = new Vector2(800 + 30, 512 - 120);
            bat.Texture = texSprites;
            Enemies.Add(bat);

            bee = new Actor.NpcBee();
            bee.Location = new Vector2(800 + 120, 512 - 80);
            bee.Texture = texSprites;
            Enemies.Add(bee);

            fly = new Actor.NpcFly();
            fly.Location = new Vector2(800 + 140, 512 - 40);
            fly.Texture = texSprites;
            Enemies.Add(fly);

            bat = new Actor.NpcBat();
            bat.Location = new Vector2(800 + 130, 512 - 120);
            bat.Texture = texSprites;
            Enemies.Add(bat);

            var spider = new Actor.NpcSpider();
            spider.Location = new Vector2(800, 512 - 60);
            spider.Texture = texSprites;
            Enemies.Add(spider);

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
            batch.Draw(texFloor, loc, null, Color.LightGray, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.999999f);
            batch.End();

            batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            foreach (var player in Players)
            {
                player.Draw(batch, gameTime);
            }

            foreach (var obj in Enemies)
            {
                obj.Draw(batch, gameTime);
            }
            
            batch.End();

            base.Draw(batch, gameTime);
        }
    }
}

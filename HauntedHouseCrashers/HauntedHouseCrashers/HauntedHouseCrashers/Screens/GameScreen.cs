using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HauntedHouseCrashers.Screens
{
    public class GameScreen : Screen
    {
        Texture2D texSprites;
        Texture2D texFloor;

        public static bool IsDebug = false;

        List<Actor.Player> Players = new List<Actor.Player>();
        public static List<Actor.Actor> Enemies = new List<Actor.Actor>();

        public Song musicLoop;

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            texSprites = content.Load<Texture2D>("HHC");
            texFloor = content.Load<Texture2D>("Floor");
            musicLoop = content.Load<Song>("music");

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

        public bool IsInitialized = false;

        public override void Update(GameTime gameTime)
        {
            if (!IsInitialized && Players.Count == 0)
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(musicLoop);

                // init players
                int playerCount = 0;
                Actor.Player player;
                if (GamePad.GetState(PlayerIndex.One).IsConnected)
                {
                    player = new Actor.Player("blue", PlayerIndex.One);
                    player.Texture = texSprites;
                    player.Location.Y = 512 - 3 * (125 / 4);
                    Players.Add(player);
                    playerCount++;
                }
                if (GamePad.GetState(PlayerIndex.Two).IsConnected)
                {
                    player = new Actor.Player("yellow", PlayerIndex.Two);
                    player.Texture = texSprites;
                    player.Location.Y = 512 - 2 * (125 / 4);
                    Players.Add(player);
                    playerCount++;
                }
                if (GamePad.GetState(PlayerIndex.Three).IsConnected)
                {
                    player = new Actor.Player("pink", PlayerIndex.Three);
                    player.Texture = texSprites;
                    player.Location.Y = 512 - 4 * (125 / 4);
                    Players.Add(player);
                    playerCount++;
                }
                if (GamePad.GetState(PlayerIndex.Four).IsConnected)
                {
                    player = new Actor.Player("green", PlayerIndex.Four);
                    player.Texture = texSprites;
                    player.Location.Y = 512 - 1 * (125 / 4);
                    Players.Add(player);
                    playerCount++;
                }

                if (playerCount == 1)
                {
                    Players[0].Health = 4;
                }
                else if (playerCount == 2)
                {
                    Players[0].Health = 3;
                    Players[1].Health = 3;
                }
                else if (playerCount == 3)
                {
                    Players[0].Health = 2;
                    Players[1].Health = 2;
                    Players[2].Health = 2;
                }
                else if (playerCount == 4)
                {
                    Players[0].Health = 1;
                    Players[1].Health = 1;
                    Players[2].Health = 1;
                    Players[3].Health = 1;
                }

                //var bee = new Actor.NpcBee();
                //bee.Location = new Vector2(800 - 90, 512 - 80);
                //bee.Texture = texSprites;
                //Enemies.Add(bee);
                
                //var fly = new Actor.NpcFly();
                //fly.Location = new Vector2(800 - 50, 512 - 40);
                //fly.Texture = texSprites;
                //Enemies.Add(fly);
                
                //var bat = new Actor.NpcBat();
                //bat.Location = new Vector2(800 - 130, 512 - 120);
                //bat.Texture = texSprites;
                //Enemies.Add(bat);

                IsInitialized = true;
            }

            foreach (var player in Players)
            {
                player.Update(gameTime);
            }


            foreach (var obj in Enemies)
            {
                obj.Update(gameTime);
                if (obj is Actor.Fireball)
                {
                    // do nothing
                }
                else if (obj is Actor.Ghost)
                {
                    // do nothing
                }
                else
                {
                    foreach (var p in Players)
                    {
                        if (obj.Bounds.Intersects(p.Bounds))
                        {
                            p.Health--;
                            obj.ReadyToRemove = true;
                            if (p.Health < 1)
                            {
                                p.ReadyToRemove = true;
                            }
                        }
                    }
                }
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

            for (int i = 0; i < Players.Count; )
            {
                if (Players[i].ReadyToRemove)
                {
                    var ghost = new Actor.Ghost();
                    ghost.Location = Players[i].Location;
                    ghost.Texture = Players[i].Texture;
                    Enemies.Add(ghost);
                    Players.RemoveAt(i);
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
            List<Vector2> locations = new List<Vector2>();
            locations.Add(new Vector2(800 + 20, 512 - 80));
            locations.Add(new Vector2(800 + 40, 512 - 40));
            locations.Add(new Vector2(800 + 30, 512 - 120));
            locations.Add(new Vector2(800 + 120, 512 - 80));
            locations.Add(new Vector2(800 + 140, 512 - 40));
            locations.Add(new Vector2(800 + 130, 512 - 120));
            locations.Add(new Vector2(800, 512 - 60));

            var _rand = new Random();
            for (int i = 0; i < 7; i++)
            {
                var rand = _rand.Next(7);

                switch (rand)
                {
                    case 0:
                        var bee = new Actor.NpcBee();
                        bee.Location = locations[i];
                        bee.Texture = texSprites;
                        Enemies.Add(bee);
                        break;
                    case 1:
                        var fly = new Actor.NpcFly();
                        fly.Location = locations[i];
                        fly.Texture = texSprites;
                        Enemies.Add(fly);
                        break;
                    case 2:
                        var bat = new Actor.NpcBat();
                        bat.Location = locations[i];
                        bat.Texture = texSprites;
                        Enemies.Add(bat);
                        break;
                    case 3:
                        var mouse = new Actor.NpcMouse();
                        mouse.Location = locations[i];
                        mouse.Texture = texSprites;
                        Enemies.Add(mouse);
                        break;
                    case 4:
                        var snake = new Actor.NpcSnake();
                        snake.Location = locations[i];
                        snake.Texture = texSprites;
                        Enemies.Add(snake);
                        break;
                    case 5:
                        var slime = new Actor.NpcSlime();
                        slime.Location = locations[i];
                        slime.Texture = texSprites;
                        Enemies.Add(slime);
                        break;
                    case 6:
                    default:
                        var spider = new Actor.NpcSpider();
                        spider.Location = locations[i];
                        spider.Texture = texSprites;
                        Enemies.Add(spider);
                        break;
                }
            }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HauntedHouseCrashers.Actor
{
    public class Fireball : Character
    {
        public Vector2 Movement = Vector2.Zero;
        public PlayerIndex Player { get; set; }

        public Fireball(Vector2 movement) : base()
        {
            HasShadow = false;
            SpriteNameStanding = "fireball";
            SpriteNamesWalking.Add("fireball");
            IsWalking = true;
            Movement = movement;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Location += Movement;
            if (Location.X > 800 || Location.X < -80)
            {
                ReadyToRemove = true;
            }

            foreach (var enemy in Screens.GameScreen.Enemies)
            {
                if (enemy is Fireball)
                {
                    // Do Nothing;
                }
                else if (enemy.Bounds.Intersects(this.Bounds))
                {
                    enemy.Health--;
                    if (enemy.Health < 1)
                    {
                        enemy.ReadyToRemove = true;
                    }
                    else
                    {
                        enemy.Location.X += 20;
                    }
                    ReadyToRemove = true;
                }
            }
        }

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            //batch.Draw(Texture, Location, SpriteHelper.SpriteRects[SpriteNameStanding], Color.White);
            base.Draw(batch, gameTime);
        }
    }
}

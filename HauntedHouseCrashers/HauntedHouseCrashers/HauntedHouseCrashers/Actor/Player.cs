using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HauntedHouseCrashers.Actor
{
    public class Player : Character
    {
        public Player(string colorName, PlayerIndex playerIndex) : base()
        {
            this.PlayerIndex = playerIndex;
            this.SpriteNameStanding = colorName + "/alien";
            this.SpriteNamesWalking.Add(colorName + "/alien_walk1");
            this.SpriteNamesWalking.Add(colorName + "/alien_walk2");
            this.SpriteNameJumping = colorName + "/alien_jump";
        }

        public PlayerIndex PlayerIndex { get; set; }
        public int Score { get; set; }

        public double fireballCoolDown = 0.0;
        public double FIREBALL_DELAY = 1.0;

        public GamePadState gamepadPrev;
        public override void Update(GameTime gameTime)
        {
            // base.Update(gameTime);

            var gamepad = GamePad.GetState(this.PlayerIndex);
            MoveActor(gamepad.ThumbSticks.Left * 3.0f);
            IsWalking = (gamepad.ThumbSticks.Left != Vector2.Zero);
            IsJumping = (JumpElapsed > 0);
            var doJump = (
                !IsJumping &&
                gamepad.Buttons.A == ButtonState.Pressed &&
                gamepadPrev.Buttons.A != ButtonState.Pressed
            );

            gamepadPrev = gamepad;

            if (doJump)
            {
                JumpElapsed = JUMP_DURATION;
            }

            if (gamepad.ThumbSticks.Left.X > 0)
            {
                IsMirrored = false;
            }
            else if (gamepad.ThumbSticks.Left.X < 0)
            {
                IsMirrored = true;
            }

            base.Update(gameTime);

            fireballCoolDown -= gameTime.ElapsedGameTime.TotalSeconds;
            if (gamepad.Buttons.B == ButtonState.Pressed && fireballCoolDown <= 0.0)
            {
                var speed = IsMirrored ? -5 : 5;
                var fireball = new Fireball(new Vector2(speed, 0));
                fireball.Texture = this.Texture;
                fireball.Location = new Vector2(
                    this.Location.X,
                    this.Location.Y // - VerticalLocation // + 0.00001f
                );
                fireball.VerticalLocation =
                    //(int)this.Location.Y -
                    SpriteHelper.SpriteRects[this.SpriteNameStanding].Height / 2;
                fireball.Player = this.PlayerIndex;
                Screens.GameScreen.Enemies.Add(fireball);
                fireballCoolDown = FIREBALL_DELAY;
            }

            //VerticalLocation = 0;

        }

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            base.Draw(batch, gameTime);
        }

        public override void MoveActor(Vector2 delta)
        {
            base.MoveActor(delta);

            float maxX = 800;
            float minY = 512 - 125;
            float maxY = 512;

            float floorHeight = maxY - minY;
            float fromFloorBottom = maxY - Location.Y;
            float minX = 26.0f + 56.0f * (fromFloorBottom / floorHeight);

            if (Location.X < minX) { Location.X = minX; }
            if (Location.X > maxX) { Location.X = maxX; }
            if (Location.Y > maxY) { Location.Y = maxY; }
            if (Location.Y < minY) { Location.Y = minY; }
        }

    }
}

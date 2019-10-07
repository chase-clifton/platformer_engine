using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SimplePlatformer
{
    public class Jumper : Sprite
    {
        public Vector2 Movement { get; set; }
        public Vector2 oldPosition;
        public Vector2 targetPosition;

        public Vector2 targetSpeed;
        public Vector2 maxSpeed = new Vector2(400, 500);
        public Vector2 curSpeed;

        private float walkSpeed = 400f;
        private float jumpSpeed = 700f;
        private float gravity = 800f;

        private float maxJumpTime = 0.3f;
        private float currentJumpTime = 0f;
        private bool isSpaceHeld = false;
        private bool isJumping = false;
        private float acceleration = 0.12f;

        public Jumper(Texture2D texture, Vector2 position, iVisible visible) : base(texture, position, visible)
        {
            
        }

        private void StopMovingIfBlocked()
        {
            Vector2 lastMovement = Position - oldPosition;
            if (lastMovement.X == 0)
            {
                Movement *= Vector2.UnitY;
                targetSpeed.X = 0;
            }
            if (lastMovement.Y == 0)
            {
                targetSpeed.Y = 0;
                Movement *= Vector2.UnitX;
                isJumping = false;
            }
        }

        public void Update(GameTime gameTime)
        {
            CheckKeyboardAndUpdateMovement();
            MoveAsFarAsPossible(gameTime);
            StopMovingIfBlocked();

            if (isJumping)
            {
                currentJumpTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            } else
            {
                currentJumpTime = 0f;
            }
        }

        private void MoveAsFarAsPossible(GameTime gameTime)
        {
            oldPosition = Position;
            targetPosition = getTargetPositionBasedOnMovement(gameTime);
            Position = Board.CurrentBoard.WhereCanIGetTo(oldPosition, targetPosition, Bounds);
        }

        private void CheckKeyboardAndUpdateMovement()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                targetSpeed.X = -walkSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                targetSpeed.X = walkSpeed;
            }

            if (!keyboardState.IsKeyDown(Keys.Right) && !keyboardState.IsKeyDown(Keys.Left))
            {
                if (IsOnFirmGround())
                {
                    targetSpeed.X = 0;
                } else
                {
                    targetSpeed.X = curSpeed.X;
                }
            }

            if (!isSpaceHeld && keyboardState.IsKeyDown(Keys.Space) )
            {
                if (IsOnFirmGround() && currentJumpTime <= maxJumpTime)
                {
                    isSpaceHeld = true;
                    isJumping = true;
                    targetSpeed.Y = -jumpSpeed;
                }
            }

            if (currentJumpTime > maxJumpTime)
            {
                isJumping = false;
            }

            if (isSpaceHeld && !keyboardState.IsKeyDown(Keys.Space))
            {
                isSpaceHeld = false;
                isJumping = false;
            }

            if (!isJumping)
            {
                if (IsOnFirmGround())
                {
                    curSpeed.Y = targetSpeed.Y = 0;
                }
                else
                {
                    targetSpeed.Y = gravity;
                }
            }

            curSpeed = acceleration * targetSpeed + (1 - acceleration) * curSpeed;
            if (curSpeed.X > maxSpeed.X)
            {
                // TODO -- Add way to go beyond max horizontal speed (e.g. boosters, wind, etc.)
                curSpeed.X = maxSpeed.X;
            }

            curSpeed.Y = 0.3f * targetSpeed.Y + (1 - 0.3f) * curSpeed.Y;
            if (curSpeed.Y > maxSpeed.Y)
            {
                curSpeed.Y = maxSpeed.Y;
            }
        }

        private Vector2 getTargetPositionBasedOnMovement(GameTime gameTime)
        {
            return oldPosition + curSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
        }

        public void Draw()
        {
            _v.Draw(Texture, Position);
        }

    }
}

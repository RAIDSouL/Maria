using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maria.Models;
using Maria.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Maria.Sprites
{
    public class Player : Sprite
    {
        
        public float Speed = 1f;
        private Vector2 gravityVelocity = new Vector2(0, 0);

        public override Rectangle Rectangle()
        {
            return new Rectangle((int)Position.X + 4, (int)Position.Y, (animations.ElementAt(0).Value.Texture.Width / animations.ElementAt(0).Value.FrameCount) - 4, animations.ElementAt(0).Value.Texture.Height);
        }

        public Player(Dictionary<string, Animation> _animation, float _gravity)
           : base(_animation, _gravity)
        {
            animations = _animation;
            animationManager = new AnimationManager(animations.First().Value);
            gravity = _gravity;
        }

        public Player(Texture2D texture)
            : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

            base.Update(gameTime, sprites);
            
            if (Keyboard.GetState().IsKeyDown(Input.Right))
                translation.X = Speed;   
            else if (Keyboard.GetState().IsKeyDown(Input.Left)) translation.X = -Speed;
            else translation.X = 0;

            // translation.X = Speed;
            if (ishit)
            {
                Game1.Instance.soundeffects[1].Play();
                Position = Vector2.Zero;

            }

            if (Keyboard.GetState().IsKeyDown(Input.Jump) && grounded)
                Jump(1.4f);

            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;

                //collide with box
                /*
                if (this.Velocity.X > 0 && this.IsTouchingLeft(sprite) || this.Velocity.X > 0 && this.IsTouchingRight(sprite))
                    this.Velocity.X = 0;

                if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite) || this.Velocity.Y > 0 && this.IsTouchingBottom(sprite))
                    this.Velocity.Y = 0;
                    */
            }
            Position += Velocity;
            Velocity = Vector2.Zero;

        }
        public override void Physics(List<Sprite> sprites)
        {
            
            if (physicsType == EPhysics.Dynamic)
            {
                if (jumpForce > 0)
                {
                    gravityVelocity.Y -= jumpForce;
                    jumpForce -= gravity / 60;
                }

                // On ground remove gravity
                if (grounded)
                {
                    if (jumpForce <= 0)
                        gravityVelocity.Y = 0;
                }
                else
                {
                    gravityVelocity.Y += gravity / 60;
                }

                Position += Velocity + gravityVelocity + translation;

                int groundCount = 0;
                ishit = false;
                foreach (var sprite in sprites)
                {
                    if (sprite != this) // Not check self
                    {
                        if (IsTouchingTop(sprite))
                        {
                            groundCount++;
                            // FIX: sprite fall into the block
                            if (this.Position.Y + this.Velocity.Y + this.gravityVelocity.Y > sprite.Rectangle().Top - 2)
                                this.Position = new Vector2(this.Position.X, sprite.Rectangle().Top - 2);
                        }
                        if (IsTouchingLeft(sprite) || Position.Y > 1000)
                        {
                            ishit = true;
                        }

                    }
                }
                if (groundCount > 0)
                {
                    grounded = true;
                }
                else grounded = false;
            }
        }
    }
}

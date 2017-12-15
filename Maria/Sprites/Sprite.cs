using Maria.Managers;
using Maria.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maria.Sprites
{
    public class Sprite
    {
        #region Fields;

        protected AnimationManager animationManager;

        protected Dictionary<string, Animation> animations;

        protected Vector2 _position;

        public Texture2D _texture;

        public Rectangle cropTexture;
        public bool crop;

        public Texture2D Texture { get { return _texture; } }

        public float jumpForce;

        private float jumpVelocity;

        private Vector2 gravityVelocity = new Vector2(0, 0);

        public EPhysics physicsType;

        public bool grounded;

        public bool visible = true; 

        #endregion

        #region Propoties

        public Input Input;

        public Vector2 Position
        {
            get{ return _position;}
            set
            {
                _position = value;

                if (animationManager != null)
                    animationManager.Position = _position;

            }
        }

        public float Speed = 1f;

        public Vector2 translation;
        public Vector2 Velocity;

        public float gravity;

        public bool isSolid;

        public Color Color = Color.White;



        #endregion

        #region Overload

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            gravity = 0;
        }

        public Sprite(Dictionary<string, Animation> animation)
        {
            animations = animation;
            animationManager = new AnimationManager(animations.First().Value);
        }

        public Sprite(Dictionary<string, Animation> _animation, float _gravity)
        {
            animations = _animation;
            animationManager = new AnimationManager(animations.First().Value);
            gravity = _gravity;
        }


        #endregion

        #region Method

        public virtual Rectangle Rectangle ()
        {
            if (animations != null)
              return new Rectangle((int)Position.X, (int)Position.Y, animations.ElementAt(0).Value.Texture.Width / animations.ElementAt(0).Value.FrameCount, animations.ElementAt(0).Value.Texture.Height / animations.ElementAt(0).Value.FrameCount);
            if (crop)
             return new Rectangle((int)Position.X, (int)Position.Y, cropTexture.Width, cropTexture.Height);
             
            return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null && animationManager == null)
            {
                if (crop == false)
                    spriteBatch.Draw(_texture, Position, Color);
                else spriteBatch.Draw(_texture, Position, cropTexture, Color);
            }
            else if (animationManager != null)
                animationManager.Draw(spriteBatch);
            else throw new Exception("This ain't right...");
        }

        public virtual void Move()
        {
            Velocity.X = Speed;
        }

        protected virtual void SetAnimation()
        {
            if (animations != null)
                animationManager.Play(animations["bunny"]);
        }


        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //Move();

            SetAnimation();
            if (animations != null)
                animationManager.Update(gameTime);

            // Physics
            Physics(sprites);

        }

        public virtual void Physics (List<Sprite> sprites)
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
                foreach (var sprite in sprites)
                {
                    if (sprite != this) // Not check self
                    {
                        if (IsTouchingTop(sprite))
                        {
                            if (sprite.isSolid)
                            {
                                groundCount++;
                                // FIX: sprite fall into the block
                                if (Position.Y + Velocity.Y + gravityVelocity.Y > sprite.Rectangle().Top - 2)
                                    Position = new Vector2(Position.X, sprite.Rectangle().Top - 2);
                            }
                            OnTouchingBottom(sprite);
                        }
                        if (IsTouchingLeft(sprite)) OnTouchingRight(sprite);
                        if (IsTouchingRight(sprite)) OnTouchingLeft(sprite);
                    }
                }
                if (groundCount > 0) grounded = true;
                else grounded = false;
            }
        }

        public virtual void Jump()
        {
            Jump(jumpForce);
        }

        public virtual void Jump(float force)
        {
            Console.WriteLine(jumpForce);
            if (force < jumpForce || jumpForce > 0) return;
            if (grounded)
            {
                if (jumpForce <= 0)
                    Game1.Instance.soundeffects[0].Play();
                jumpForce = force;
                grounded = false;
            }
        }

        public virtual void OnTouchingLeft (Sprite sprite) { }
        public virtual void OnTouchingRight(Sprite sprite) { }
        public virtual void OnTouchingTop(Sprite sprite) { }
        public virtual void OnTouchingBottom(Sprite sprite) { }


        #endregion

        #region Collision

        protected virtual bool IsTouchingLeft(Sprite sprite)
        {
            return  this.Rectangle().Right + this.Velocity.X > sprite.Rectangle().Left &&
                    this.Rectangle().Left < sprite.Rectangle().Left &&
                    this.Rectangle().Bottom > sprite.Rectangle().Top &&
                    this.Rectangle().Top < sprite.Rectangle().Bottom;
        }

        protected virtual bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle().Left + this.Velocity.X < sprite.Rectangle().Right &&
                    this.Rectangle().Right > sprite.Rectangle().Right &&
                    this.Rectangle().Bottom > sprite.Rectangle().Top &&
                    this.Rectangle().Top < sprite.Rectangle().Bottom;
        }

        protected virtual bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle().Bottom + this.Velocity.Y + this.gravityVelocity.Y > sprite.Rectangle().Top - 2 &&
                   this.Rectangle().Top < sprite.Rectangle().Top &&
                   this.Rectangle().Right > sprite.Rectangle().Left &&
                   this.Rectangle().Left < sprite.Rectangle().Right;
        }

        protected virtual bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle().Top + this.Velocity.Y > sprite.Rectangle().Bottom &&
                    this.Rectangle().Bottom > sprite.Rectangle().Bottom &&
                    this.Rectangle().Right > sprite.Rectangle().Left &&
                    this.Rectangle().Left < sprite.Rectangle().Right;
        }

        #endregion
    }
}

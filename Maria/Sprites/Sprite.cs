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

        public bool ishit;

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

        public Color Color = Color.White;

        public Rectangle Rectangle
        {
            get
            { 
                if (animations != null)
                   return new Rectangle((int)Position.X, (int)Position.Y, animations.ElementAt(0).Value.Texture.Width / animations.ElementAt(0).Value.FrameCount, animations.ElementAt(0).Value.Texture.Height / animations.ElementAt(0).Value.FrameCount);
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
                

            }

        }

        #endregion

        #region Method

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

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            gravity = 0;
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //Move();

            SetAnimation();
            if (animations != null)
                animationManager.Update(gameTime);

            Position += Velocity + gravityVelocity + translation;

            // Gravity
            if (physicsType == EPhysics.Dynamic)
            {
                if (jumpForce > 0)
                {
                    gravityVelocity.Y -= jumpForce;
                    jumpForce -= gravity / 60;
                }

                if (grounded && jumpForce <= 0)
                {
                    if (Velocity.Y > 0)
                        Velocity.Y = 0;
                    gravityVelocity.Y = 0;
                }
                else
                {
                    if (jumpForce <= 0)
                        gravityVelocity.Y += gravity / 60;
                }
                grounded = false;
                ishit = false;
                foreach (var sprite in sprites)
                {
                    if (sprite != this) // Not check self
                    {
                        if (IsTouchingTop(sprite))
                        {
                            grounded = true;
                        }
                        if (IsTouchingLeft(sprite))
                        {
                            ishit = true;
                        }
                        
                    }
                }
            }

        }

        #endregion

        #region Collision
        
        protected bool IsTouchingLeft(Sprite sprite)
        {
            return  this.Rectangle.Right + this.Velocity.X > sprite.Rectangle.Left &&
                    this.Rectangle.Left < sprite.Rectangle.Left &&
                    this.Rectangle.Bottom > sprite.Rectangle.Top &&
                    this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.Velocity.X < sprite.Rectangle.Right &&
                    this.Rectangle.Right > sprite.Rectangle.Right &&
                    this.Rectangle.Bottom > sprite.Rectangle.Top &&
                    this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Velocity.Y + this.gravityVelocity.Y > sprite.Rectangle.Top - sprite.Rectangle.Height &&
                   this.Rectangle.Top < sprite.Rectangle.Top &&
                   this.Rectangle.Right > sprite.Rectangle.Left &&
                   this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top + this.Velocity.Y > sprite.Rectangle.Bottom &&
                    this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
                    this.Rectangle.Right > sprite.Rectangle.Left &&
                    this.Rectangle.Left < sprite.Rectangle.Right;
        }

        #endregion
    }
}

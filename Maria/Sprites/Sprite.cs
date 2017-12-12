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

        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;

        protected Vector2 _position;

        public Texture2D _texture;

        public Texture2D Texture { get { return _texture; } }

        #endregion

        #region Propoties

        public Input Input;

        public Vector2 Position
        {
            get{ return _position;}
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;

            }
        }

        public float Speed = 1f;

        public Vector2 Velocity;

        public float gravity;

        public Color Color = Color.White;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

       


        #endregion

        #region Method

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else throw new Exception("This ain't right...");
        }

        public virtual void Move()
        {
            Velocity.X = Speed;
        }

        protected virtual void SetAnimation()
        {
            _animationManager.Play(_animations["bunny"]);
        }

        public Sprite(Dictionary<string, Animation> animation)
        {
            _animations = animation;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public Sprite(Dictionary<string, Animation> animation, float _gravity)
        {
            _animations = animation;
            _animationManager = new AnimationManager(_animations.First().Value);
            gravity = _gravity;
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;

        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();

            SetAnimation();

            _animationManager.Update(gameTime);

            Position += Velocity;

            // Gravity
            if (gravity != 0 || !IsTouchingBottom(this))
                Velocity.Y += gravity / 60;

            //Velocity = Vector2.Zero;
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
            return this.Rectangle.Bottom + this.Velocity.Y < sprite.Rectangle.Top &&
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

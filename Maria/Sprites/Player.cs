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
                translation.X = -Speed;
                Game1.Instance.soundeffects[1].Play();
                Position = Vector2.Zero;

            }

            if (Keyboard.GetState().IsKeyDown(Input.Jump) && grounded)
                Jump(1f);

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
    }
}

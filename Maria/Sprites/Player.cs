using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maria.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Maria.Sprites
{
    public class Player : Sprite
    {
        public float Speed = 0.1f;

        public Player(Dictionary<string, Animation> _animation, float _gravity)
           : base(_animation, _gravity)
        {

        }

        public Player(Texture2D texture)
            : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Velocity.X += 0.1f;
                Console.WriteLine("Press");
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Right))
                Velocity.X = 0;

            if (Keyboard.GetState().IsKeyDown(Keys.Left)) Velocity.X -= Speed;
            else Velocity.X = 0;

            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;
                //collide with box
                /*
                if (this.Velocity.X > 0 && this.IsTouchingLeft(sprite) || this.Velocity.X > 0 && this.IsTouchingRight(sprite))
                    this.Velocity.X = 0;

                if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite) || this.Velocity.X > 0 && this.IsTouchingRight(sprite))
                    this.Velocity.Y = 0;
                    */
            }
        }
    }
}

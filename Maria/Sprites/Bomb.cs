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
    public class Bomb : Sprite
    {
        public Bomb(Texture2D texture) : base(new Dictionary<string, Animation>() { { "bunny", new Animation(texture, 4) }, })
        {
            _texture = texture;
        }

        public override Rectangle Rectangle()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width / 4, _texture.Height);
        }

    }
}

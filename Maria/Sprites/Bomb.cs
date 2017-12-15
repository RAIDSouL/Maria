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
            return new Rectangle((int)Position.X + 5, (int)Position.Y + 5, (_texture.Width / 4) - 10, (_texture.Height) - 10);
        }

    }
}

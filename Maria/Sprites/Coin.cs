using Maria.Models;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maria.Sprites
{
    public class Coin : Sprite
    {
        public Coin (Texture2D _texture) : base(new Dictionary<string, Animation>() { { "bunny", new Animation(_texture, 7) }, })
        {

        }

    }
}

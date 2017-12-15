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
    public class Coin : Sprite
    {

        public bool collected;

        public Coin (Texture2D texture) : base(new Dictionary<string, Animation>() { { "bunny", new Animation(texture, 7) }, })
        {
            _texture = texture;
        }

        public override Rectangle Rectangle()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width / 7, _texture.Height);
        }

        public void Collect()
        {
            if (!collected)
            {
                collected = true;
                Game1.Instance.player.score += 10;
                Destroy();
            }
        }

    }
}

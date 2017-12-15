using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maria.Sprites
{
    public class Endblock : Sprite
    {
        public Rectangle area;

        public Endblock (Texture2D texture) : base(texture)
        {
            _texture = texture;
            gravity = 0;
            visible = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public override Rectangle Rectangle()
        {
            return area;
        }
    }
}

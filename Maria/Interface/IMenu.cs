using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maria
{
    public interface IMenu
    {
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace Maria
{
    class Camera
    {
        public Matrix tranform;
        Viewport view;
        Vector2 centre;

        float zoom = 2;

        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            centre = new Vector2(game.player.Position.X + (game.spriteRectangle.Width / 2 - 320)/zoom + 50 , 0);
            
            tranform = Matrix.CreateScale(new Vector3(1 * zoom, 1 * zoom, 0)) *
                Matrix.CreateTranslation(new Vector3(zoom * -centre.X, zoom * -centre.Y, 0)) ; 
           
        }
    }
}

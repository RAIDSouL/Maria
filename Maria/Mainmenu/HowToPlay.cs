using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maria.Mainmenu
{
    public class HowToPlay
    {
        Texture2D mouse;
        Texture2D bg3;
        Texture2D HT;
        Texture2D next;
        Rectangle nextb;
        Rectangle mouseB;

        Vector2 baseScreen = new Vector2(1000, 1000);

        public HowToPlay ()
        {
            bg3 = Game1.Instance.Content.Load<Texture2D>("mainmenu/Bg3");
            HT = Game1.Instance.Content.Load<Texture2D>("mainmenu/HT");
            mouse = Game1.Instance.Content.Load<Texture2D>("mainmenu/m");
            next = Game1.Instance.Content.Load<Texture2D>("mainmenu/nextb");
        }

        public void Update (GameTime gameTime)
        {
            nextb = PortRectangle(700, 800, 300, 200);
            mouseB = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 30, 30);
            if (mouseB.Intersects(nextb) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                MainMenu.Instance.ChangeStage(5); // Play
            }
        }

        public void Draw (GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bg3, PortRectangle(0, 0, 1200, 1000), Color.White);
            spriteBatch.Draw(HT, PortRectangle(100, 100, 800, 700), Color.White);
            spriteBatch.Draw(next, nextb, Color.White);
            spriteBatch.Draw(mouse, mouseB, Color.White);
        }

        private Rectangle PortRectangle(int x, int y, int width, int height)
        {
            return PortRectangle(new Rectangle(x, y, width, height));
        }

        private Rectangle PortRectangle(Rectangle baseRectangle)
        {
            return new Rectangle(
                baseRectangle.X * Game1.Instance.GraphicsDevice.Viewport.Width / (int)baseScreen.X,
                baseRectangle.Y * Game1.Instance.GraphicsDevice.Viewport.Height / (int)baseScreen.Y,
                baseRectangle.Width * Game1.Instance.GraphicsDevice.Viewport.Width / (int)baseScreen.X,
                baseRectangle.Height * Game1.Instance.GraphicsDevice.Viewport.Height / (int)baseScreen.Y
                );
        }
    }
}

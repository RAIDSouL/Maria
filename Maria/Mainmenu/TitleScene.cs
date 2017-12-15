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
    public class TitleScene
    {
        Texture2D bg;
        Texture2D character;
        Texture2D logo;
        Texture2D start;
        Texture2D mouse;
        Vector2 baseScreen = new Vector2(1000, 1000);

        private Rectangle howtoB;
        private Rectangle mouseB;

        public TitleScene()
        {
            bg = Game1.Instance.Content.Load<Texture2D>("mainmenu/Bg");
            logo = Game1.Instance.Content.Load<Texture2D>("mainmenu/Logo");
            character = Game1.Instance.Content.Load<Texture2D>("mainmenu/Bg1");
            start = Game1.Instance.Content.Load<Texture2D>("mainmenu/start");
            mouse = Game1.Instance.Content.Load<Texture2D>("mainmenu/m");
        }

        public void Update (GameTime gameTime)
        {
            howtoB = PortRectangle(550, 275, 350, 300);
            mouseB = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 30, 30);
            if (mouseB.Intersects(howtoB) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Game1.Instance.soundeffects[2].Play();
                MainMenu.Instance.ChangeStage(1);
            }
        }

        public void Draw (GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bg, new Rectangle(0, 0, Game1.Instance.GraphicsDevice.Viewport.Width, Game1.Instance.GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.Draw(character, PortRectangle(80, 100, 450, 600), Color.White);
            spriteBatch.Draw(logo, PortRectangle(550, 10, 400, 230), Color.White);

            spriteBatch.Draw(start, howtoB, Color.White);
            spriteBatch.Draw(mouse, mouseB, Color.White);
        }

        private Rectangle PortRectangle(int x, int y, int width, int height)
        {
            return PortRectangle(new Rectangle(x, y, width, height));
        }

        private Rectangle PortRectangle (Rectangle baseRectangle)
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

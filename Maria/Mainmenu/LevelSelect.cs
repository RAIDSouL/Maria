using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maria.Mainmenu
{
    public class LevelSelect : IMenu
    {
        Texture2D button;

        Texture2D mouse;
        Rectangle mouseB;

        Vector2 baseScreen = new Vector2(1000, 1000);

        public LevelSelect()
        {
            button = Game1.Instance.Content.Load<Texture2D>("mainmenu/nextb");
            mouse = Game1.Instance.Content.Load<Texture2D>("mainmenu/m");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            int row = 0;
            int column = 0;
            for (int i = 0; i < LevelList.list.Length; i++)
            {
                spriteBatch.Draw(button, new Rectangle(20 + 100 * column, 20 + 100 * row, 100, 100), Color.White);
                spriteBatch.DrawString(Game1.Instance.File, LevelList.list[i].map, new Vector2(20 + 100 * column, 20 + 100 * row), Color.Black);
                column++;
                if (column >= 5)
                {
                    row++;
                    column = 0;
                }
            }
            
            spriteBatch.Draw(mouse, mouseB, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            int row = 0;
            int column = 0;
            for (int i = 0; i < LevelList.list.Length; i++)
            {
                if (mouseB.Intersects(new Rectangle(20 + 100 * column, 20 + 100 * row, 100, 100)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    MainMenu.Instance.Play(i);
                }
                column++;
                if (column >= 5)
                {
                    row++;
                    column = 0;
                }
            }

            mouseB = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 30, 30);
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

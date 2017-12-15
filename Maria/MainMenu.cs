using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maria.Mainmenu;

namespace Maria
{
    public class MainMenu
    {
        public bool Active { get; private set; }

        ContentManager content;

        int stage = 0;

        private TitleScene titleScene;

        public MainMenu ()
        {
            Active = false;
            titleScene = new TitleScene();
        }

        public void Update (GameTime gameTime)
        {

        }

        public void LoadContent ()
        {

        }

        public void Draw (GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (stage == 0)
            {
                titleScene.Draw(gameTime, spriteBatch);
            }
        }

        public void ChangeStage (int _stage)
        {
            stage = _stage;
        }

    }
}

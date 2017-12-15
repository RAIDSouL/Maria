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
        public static MainMenu Instance { get; private set; }

        public bool Active { get; set; }

        ContentManager content;

        int stage = 0;

        private TitleScene titleScene;
        private HowToPlay howto;
        private LevelSelect levelSelect;

        public MainMenu ()
        {
            Instance = this;
            Active = true;
            titleScene = new TitleScene();
            howto = new HowToPlay();
            levelSelect = new LevelSelect();
        }

        public void Update (GameTime gameTime)
        {
            if (stage == 0)
                titleScene.Update(gameTime);
            else if (stage == 1)
                howto.Update(gameTime);
            else if (stage == 2)
                levelSelect.Update(gameTime);
        }

        public void Draw (GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (stage == 0)
                titleScene.Draw(gameTime, spriteBatch);
            else if (stage == 1)
                howto.Draw(gameTime, spriteBatch);
            else if (stage == 2)
                levelSelect.Draw(gameTime, spriteBatch);
        }

        public void ChangeStage (int _stage)
        {
            stage = _stage;
            if (_stage == 5)
                Active = false;
        }

        public void Play (int level)
        {
            ChangeStage(5);
            LevelList.Resetp();
            LevelList.list[level].pmap = true;
            Game1.Instance.PlayLevel(level);
            
            
        }
    }
}

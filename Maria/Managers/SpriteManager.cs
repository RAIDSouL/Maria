using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maria.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maria.Managers
{
    public class SpriteManager
    {
        public static SpriteManager Instance;

        public List<Sprite> List { get; private set; }

        public List<Texture2D> DebugBox = new List<Texture2D>();
        
        public SpriteManager ()
        {
            Instance = this;
            List = new List<Sprite>();
        }

        public void AddSprite (Sprite _sprite)
        {
            List.Add(_sprite);
        }

        public void Update (GameTime gameTime)
        {
            for (int i = List.Count - 1; i >= 0; i--)
            {
                Sprite sprite = List[i];
                sprite.Update(gameTime, List);
            }
        }

        public void Draw (GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = List.Count - 1; i >= 0; i--)
            {
                Sprite sprite = List[i];
                if (sprite.visible && sprite.GetType() != typeof(Player))
                {
                    sprite.Draw(spriteBatch);

                    //spriteBatch.Draw(DebugBox[0], sprite.Rectangle(), Color.White);
                }
            }


            // Render player alone
            if (Game1.Instance.player.visible)
            {
                Game1.Instance.player.Draw(spriteBatch);

                //spriteBatch.Draw(DebugBox[0], Game1.Instance.player.Rectangle(), Color.White);
            }
        }

        public void Destroy (Sprite _sprite)
        {
            List.Remove(_sprite);
            _sprite = null;
        }

        public void DestroyMap ()
        {
            DestroyAllBlock();
            for (int i = List.Count - 1; i >= 0; i--)
            {
                Sprite sprite = List[i];
                if (sprite.GetType() == typeof(Player)) continue;
                   
                List[i] = null;
                List.RemoveAt(i);
            }
        }

        public void DestroyAllBlock ()
        {
            for (int i = List.Count - 1; i >= 0; i--)
            {
                Sprite sprite = List[i];
                if (sprite.GetType() == typeof(Block))
                {
                    List[i] = null;
                    List.RemoveAt(i);
                }
            }
        }

    }
}

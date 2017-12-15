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
            foreach (var sprite in List)
                sprite.Update(gameTime, List);
        }

        public void Draw (GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var sprite in List)
            {
                sprite.Draw(spriteBatch);

                spriteBatch.Draw(DebugBox[0], sprite.Rectangle(), Color.White);
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
            Console.WriteLine("Count: " + List.Count);
        }

    }
}

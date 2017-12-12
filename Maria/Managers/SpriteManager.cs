﻿using System;
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
            }
        }

    }
}

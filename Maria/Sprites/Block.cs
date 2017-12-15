using Maria.Enum;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maria.Sprites
{
    public class Block : Sprite
    {
        public EBlock blockType;

        public Block(Texture2D texture, int _blockType) : base(texture)
        {
            _texture = texture;
            gravity = 0;
            physicsType = EPhysics.Static;
            blockType = (EBlock)_blockType;
        }
    }
}

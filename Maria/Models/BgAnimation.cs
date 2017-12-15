using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maria.Models
{
    public class BgAnimation
    {
        public int CurrentFrame { get; set; }

        public int FrameCount { get; private set; }

        public int FrameHeight { get { return Texture.Height / FrameCount; } }

        public float FrameSpeed { get; set; }

        public int FrameWidth { get { return Texture.Width; } }

        public bool Islooping { get; set; }

        public Texture2D Texture { get; private set; }

        public BgAnimation(Texture2D texture, int frameCount)
        {
            Texture = texture;

            FrameCount = frameCount;

            Islooping = true;

            FrameSpeed = 1 / 12f;
        }
    }
}

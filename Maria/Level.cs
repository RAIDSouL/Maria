using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maria
{
    public struct Level
    {
        public string map;
        public bool playing_1;
        public bool playing_2;
        public bool playing_3;
        public bool playing_4;
    }

    public static class LevelList
    {
        public readonly static Level[] list =
        {
            new Level { map = "level1" },
            new Level { map = "level2" },
            new Level { map = "level3" },
            new Level { map = "level4" },
            new Level { map = "level5" },
        };
    }
}

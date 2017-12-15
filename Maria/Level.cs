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
        public bool pmap;

    }

    public static class LevelList
    {
        public readonly static Level[] list =
        {
            new Level { map = "level1" , pmap = false},
            new Level { map = "level2" , pmap = false},
            new Level { map = "level3" , pmap = false},
            new Level { map = "level4" , pmap = false},
            new Level { map = "level5" , pmap = false},
        };

        public static void Resetp()
        {
            for(int i = 0; i< list.Length; i++)
            {
                list[i].pmap = false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maria
{
    public class GameObjectManager
    {
        public static List<GameObject> list = new List<GameObject>();

        public static void Add (GameObject gameObject)
        {
            list.Add(gameObject);
        }
    }
}

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Maria
{
    public class GameObjectManager
    {
        public static List<GameObject> list = new List<GameObject>();

        public static void Add (GameObject gameObject)
        {
            list.Add(gameObject);
        }

        public void Update ()
        {

        }

        public void Draw (GameTime gameTime)
        {

        }
    }
}
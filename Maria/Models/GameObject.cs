using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Maria;

namespace Maria
{
    public class GameObject
    {
        public Vector3 Position;

        public GameObject ()
        {
            GameObjectManager.list.Add(this);
        }

        public virtual void Update ()
        {

        }

        public virtual void Draw (GameTime gameTime)
        {

        }

    }
}

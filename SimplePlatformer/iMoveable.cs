using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePlatformer
{
    interface iMoveable
    {
        void Move();
    }

    public class Walkable : iMoveable
    {
        public void Move()
        {

        }
    }

}

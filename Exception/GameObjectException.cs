using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.Exception
{
    class GameObjectException : ArgumentException
    {
        public GameObjectException()
            : base()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orbit
{
    class OrbitException : Exception
    {
        public OrbitException(string message): base(message) { }
    }
}

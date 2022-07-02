using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class WordIsNotFound : Exception
    {
        public WordIsNotFound() : base()
        {
        }

        public WordIsNotFound(string message) : base(message)
        {
        }


    }
}

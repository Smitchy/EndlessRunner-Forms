using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMD2Project___endless_running
{
    public static class RandomHelper
    {
        public static Random rand;

        static RandomHelper()
        {
            rand = new Random();
        }
    }
}

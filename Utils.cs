using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSZI_2018
{
    public static class Utils
    {
        private static Random rnd = new Random();
        public static int GetRandom(int begin, int end) => rnd.Next(begin, end);
    }
}

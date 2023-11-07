using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcdLib
{
    public static class Euclid
    {
        public static int GCD(params int[] inp) => inp.Aggregate(EuclideanGCD);
        private static int EuclideanGCD(int a, int b)
        {
            if (a == 0) return b;
            while (b != 0)
            {
                if (a > b) a -= b;
                else b -= a;
            }
            return a;
        }
    }
}

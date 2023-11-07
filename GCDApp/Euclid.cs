using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCDApp
{
    public static class Euclid
    {
        public static int GCD(params int[] inp)
        {
            if (inp.Length == 1) return inp[0];
            if (inp.Length == 2) return EuclideanGCD(inp[0], inp[1]);
            int[] a = inp.Skip(0).Take(inp.Length / 2).ToArray();
            int[] b = inp.Skip(inp.Length / 2).Take(inp.Length % 2 == 0 ? inp.Length / 2 : (inp.Length / 2) + 1).ToArray();
            return GCD(GCD(a), GCD(b));
        }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public static class GenerateRandom
    {

        public static List<uint> GenerateRandomArray1(uint n)
        {
            return (from num in Enumerable.Repeat(0, (int)n) let rnd = new Random(Guid.NewGuid().GetHashCode()) select (uint)rnd.Next(0, (int)n)).OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RandomGenerator
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random _random;

        public RandomGenerator()
        {
            _random = new Random();
        }

        public int GetInt(int a, int b)
        {
            return _random.Next(a, b);
        }

        public int GetInt(int max)
        {
            return _random.Next(max);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RandomGenerator
{
    public interface IRandomGenerator
    {
        public int GetInt(int a, int b);
        public int GetInt(int max);
    }
}

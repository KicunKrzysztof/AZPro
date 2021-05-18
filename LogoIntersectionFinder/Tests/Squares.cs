using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoIntersectionFinder.Tests
{
    public class Squares : IComplexityTestInstance
    {
        public bool ExpectedResult()
        {
            return false;
        }

        public string Generate(int size)
        {
            var seq = new List<string>() { "fd 20 ", "rt 90 ", "fd 20 ", "rt 90 ", "fd 20 ", "rt 90 ", "fd 20 ", "pu ", "rt 180 ", "fd 30 ", "lt 90 ", "pd "};
            var sb = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                sb.Append(seq[i % 12]);
            }
            return sb.ToString();
        }

        public string GetName()
        {
            return "Squares";
        }
        public bool ResultIsKnown()
        {
            return true;
        }
    }
}

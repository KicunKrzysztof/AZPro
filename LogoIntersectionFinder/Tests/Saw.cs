using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoIntersectionFinder.Tests
{
    public class Saw : IComplexityTestInstance
    {
        public bool ExpectedResult()
        {
            return false;
        }

        public string Generate(int size)
        {
            var seq = new List<string>() { "fd 40 ", "rt 143 ", "fd 50 ", "lt 143 "};
            var sb = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                sb.Append(seq[i % 4]);
            }
            return sb.ToString();
        }

        public string GetName()
        {
            return "Saw";
        }
        public bool ResultIsKnown()
        {
            return true;
        }
    }
}

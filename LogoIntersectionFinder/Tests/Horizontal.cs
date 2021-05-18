using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoIntersectionFinder.Tests
{
    public class Horizontal : IComplexityTestInstance
    {
        public bool ExpectedResult()
        {
            return false;
        }

        public string Generate(int size)
        {
            var seq = new List<string>() { "rt 90 ", "fd 50 ", "pu ", "lt 90 ", "fd 5 ", "pd ", "lt 90 ", "fd 50 ", "pu ", "rt 90 ", "fd 5 ", "pd " };
            var sb = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                sb.Append(seq[i % 12]);
            }
            return sb.ToString();
        }

        public string GetName()
        {
            return "Horizontal";
        }

        public bool ResultIsKnown()
        {
            return true;
        }
    }
}

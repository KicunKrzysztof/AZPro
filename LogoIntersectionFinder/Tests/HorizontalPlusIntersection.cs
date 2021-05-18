using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoIntersectionFinder.Tests
{
    public class HorizontalPlusIntersection : IComplexityTestInstance
    {
        public bool ExpectedResult()
        {
            return true;
        }

        public string Generate(int size)
        {
            var seq = new List<string>() { "rt 90 ", "fd 50 ", "pu ", "lt 90 ", "fd 5 ", "pd ", "lt 90 ", "fd 50 ", "pu ", "rt 90 ", "fd 5 ", "pd " };
            var sb = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                sb.Append(seq[i % 12]);
            }
            sb.Append("pu ");
            if(size % 12 == 0 || size % 12 == 3 || size % 12 == 4 || size % 12 == 5 || size % 12 == 9 || size % 12 == 10 || size % 12 == 11) //zołw głową do góry
            {
                sb.Append("rt 90 fd 100 ");
            }
            else if (size % 12 == 1 || size % 12 == 2) //zołw głową w prawo
            {
                sb.Append("fd 100 ");
            }
            else //zółw głową w lewo (size % 12 == 6 || size % 12 == 7 || size % 12 == 8)
            {
                sb.Append("rt 180 fd 100 ");
            }
            sb.Append("pd fd 10 pu lt 90 fd 5 lt 90 fd 5 lt 90 pd fd 10");
            return sb.ToString();
        }

        public string GetName()
        {
            return "HorizontalWithIntersetion";
        }
        public bool ResultIsKnown()
        {
            return true;
        }
    }
}

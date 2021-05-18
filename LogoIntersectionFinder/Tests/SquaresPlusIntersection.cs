using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoIntersectionFinder.Tests
{
    public class SquaresPlusIntersection : IComplexityTestInstance
    {
        public bool ExpectedResult()
        {
            return true;
        }

        public string Generate(int size)
        {
            var seq = new List<string>() { "fd 20 ", "rt 90 ", "fd 20 ", "rt 90 ", "fd 20 ", "rt 90 ", "fd 20 ", "pu ", "rt 180 ", "fd 30 ", "lt 90 ", "pd " };
            var sb = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                sb.Append(seq[i % 12]);
            }
            sb.Append("pu ");
            if(size % 12 == 0 || size % 12 == 1 || size % 12 == 11)//zołw z głową w gore
            {
                sb.Append("rt 90 ");
            }
            else if(size % 12 == 2 || size % 12 == 3 || size % 12 == 9 || size % 12 == 10)//zołw z głową w prawo
            {
                sb.Append("");
            }
            else if(size % 12 == 4 || size % 12 == 5)//zółw z głową w dół
            {
                sb.Append("lt 90 ");
            }
            else//złółw z głową w lewo
            {
                sb.Append("rt 180 ");
            }
            sb.Append("fd 100 pd fd 10 pu lt 90 fd 5 lt 90 fd 5 lt 90 pd fd 10");
            return sb.ToString();
        }

        public string GetName()
        {
            return "SquaresWithIntersection";
        }
        public bool ResultIsKnown()
        {
            return true;
        }
    }
}

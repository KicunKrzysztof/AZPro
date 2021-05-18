using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoIntersectionFinder.Tests
{
    public class SawPlusIntersection : IComplexityTestInstance
    {
        public bool ExpectedResult()
        {
            return true;
        }

        public string Generate(int size)
        {
            var seq = new List<string>() { "fd 40 ", "rt 143 ", "fd 50 ", "lt 143 " };
            var sb = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                sb.Append(seq[i % 4]);
            }
            sb.Append("pu ");
            if(size % 4 == 0 || size % 4 == 1)//zolw z glowa w gorze
            {
                sb.Append("rt 90 ");
            }
            else// zołw pochylony w tył
            {
                sb.Append("lt 53 ");
            }
            sb.Append("fd 100 pd fd 10 pu lt 90 fd 5 lt 90 fd 5 lt 90 pd fd 10");
            return sb.ToString();
        }

        public string GetName()
        {
            return "SawWithIntersection";
        }
        public bool ResultIsKnown()
        {
            return true;
        }
    }
}

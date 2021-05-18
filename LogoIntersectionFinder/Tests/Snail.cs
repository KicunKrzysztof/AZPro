using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoIntersectionFinder.Tests
{
    public class Snail : IComplexityTestInstance
    {
        public bool ExpectedResult()
        {
            return false;
        }

        public string Generate(int size)
        {
            var sb = new StringBuilder();
            int dist = 10;
            for(int i = 0; i < size; i++)
            {
                if(i % 2 == 0)
                {
                    sb.Append("rt 90 ");
                }
                else
                {
                    sb.Append($"fd {dist} ");
                    dist += 2;
                }
            }
            return sb.ToString();
        }

        public string GetName()
        {
            return "Snail";
        }
        public bool ResultIsKnown()
        {
            return true;
        }
    }
}

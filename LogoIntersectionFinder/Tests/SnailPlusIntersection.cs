using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoIntersectionFinder.Tests
{
    public class SnailPlusIntersection : IComplexityTestInstance
    {
        public bool ExpectedResult()
        {
            return true;
        }

        public string Generate(int size)
        {
            var sb = new StringBuilder();
            int dist = 10;
            for (int i = 0; i < size; i++)
            {
                if (i % 2 == 0)
                {
                    sb.Append("rt 90 ");
                }
                else
                {
                    sb.Append($"fd {dist} ");
                    dist += 2;
                }
            }
            sb.Append("pu ");
            if(size % 8 == 0 || size % 8 == 7)//głowa do góry
            {
                sb.Append("rt 90 ");
            }
            else if(size % 8 == 1 || size % 8 == 2)//głowa w prawo
            {
                sb.Append("");
            }
            else if (size % 8 == 3 || size % 8 == 4)//głowa w dół
            {
                sb.Append("lt 90 ");
            }
            else//głowa w lewo
            {
                sb.Append("rt 180 ");
            }
            sb.Append($"fd {dist + 100} pd fd 10 pu lt 90 fd 5 lt 90 fd 5 lt 90 pd fd 10");
            return sb.ToString();
        }

        public string GetName()
        {
            return "SnailWithIntersection";
        }
        public bool ResultIsKnown()
        {
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogoIntersectionFinder.Helpers;

namespace LogoIntersectionFinder.Tests
{
    public class Random : IComplexityTestInstance
    {
        public bool ExpectedResult()
        {
            return true;//not known, look for method ResultIsKnown()
        }

        public string Generate(int size)
        {
            return RandomGenerator.Generate(size);
        }

        public string GetName()
        {
            return "Random";
        }
        public bool ResultIsKnown()
        {
            return false;
        }
    }
}

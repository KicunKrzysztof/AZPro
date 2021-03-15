using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogoIntersectionFinder.Sweep;
using LogoIntersectionFinder.Helpers;
using LogoIntersectionFinder.LogoParser;

namespace LogoIntersectionFinder
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Turtle myTurtle = new Turtle();
            //var segmentList = myTurtle.Parse("fd 50 rt 90 fd 10 rt 90 fd 10 rt 90 fd 5");
            //var segmentList = myTurtle.Parse("fd 50 rt 90 fd 10 rt 90 fd 10 rt 90 fd 50");
            var segmentList = myTurtle.Parse("fd 50 bk 10");
            if(segmentList == null)
            {
                Console.WriteLine("Błędny program");
                return;
            }
            var SweepAlgorithm = new SweepAlgorithm();
            bool intersection = SweepAlgorithm.FindIntersection(segmentList);
            Console.WriteLine($"intersection in program? {intersection}");
            Console.ReadKey();
        }
    }
}

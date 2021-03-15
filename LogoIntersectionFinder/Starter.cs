using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogoIntersectionFinder.Sweep;
using LogoIntersectionFinder.LogoParser;
using System.IO;

namespace LogoIntersectionFinder
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Console.WriteLine("########################################################");
            Console.WriteLine("########################################################");
            Console.WriteLine("LOGO INTERSECTION FINDER");
            Console.WriteLine("Acceptable logo commands:");
            Console.WriteLine("pu");
            Console.WriteLine("pd");
            Console.WriteLine("fd X");
            Console.WriteLine("bk X");
            Console.WriteLine("lt X");
            Console.WriteLine("rt X");
            Console.WriteLine("Key in logo program here, or file with semicolon-separated programs");
            Console.WriteLine("Key in \"exit\" to close");
            Console.WriteLine("########################################################");
            Console.WriteLine("########################################################");
            
            Turtle myTurtle = new Turtle();
            SweepAlgorithm SweepAlgorithm = new SweepAlgorithm();

            while (true)
            {
                string input = Console.ReadLine();
                if (input.Equals("exit"))
                    break;
                string program;
                try
                {
                    program = File.ReadAllText(input);
                }
                catch
                {
                    program = "Error filename";
                }
                if (program.Equals("Error filename"))
                {
                    program = input;
                }
                var programs = program.Split(';').ToList();
                foreach(string p in programs)
                {
                    Console.WriteLine("--------------------------------------------------------");
                    Console.WriteLine($"Program logo: \"{p}\"");
                    var segmentList = myTurtle.Parse(p);
                    if(segmentList == null)
                    {
                        Console.WriteLine("Zawiera błąd");
                        continue;
                    }
                    bool intersection = SweepAlgorithm.FindIntersection(segmentList);
                    if(intersection)
                        Console.WriteLine("Zawiera przecięcie");
                    else
                        Console.WriteLine("Nie zawiera przecięcia");
                }
            }
        }
    }
}

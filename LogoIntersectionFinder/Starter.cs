using System;
using System.Text;
using LogoIntersectionFinder.Sweep;
using LogoIntersectionFinder.LogoParser;
using LogoIntersectionFinder.Helpers;
using LogoIntersectionFinder.Tests;
using System.IO;
using System.Linq;

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
            Console.WriteLine("Key in \"filegen\" to generate file");
            Console.WriteLine("Key in \"comp\" for complexity tests");
            Console.WriteLine("########################################################");
            Console.WriteLine("########################################################");

            Turtle myTurtle = new Turtle();
            SweepAlgorithm SweepAlgorithm = new SweepAlgorithm();

            StringBuilder logBuilder = new StringBuilder();
            logBuilder.AppendLine();
            logBuilder.AppendLine("########################################################");
            logBuilder.AppendLine("########################################################");
            while (true)
            {
                string input = Console.ReadLine();
                if (input.Equals("exit"))
                    break;
                if (input.Equals("filegen"))
                {
                    Console.WriteLine("Write number of logo commends for file");
                    logBuilder.AppendLine("Write number of logo commends for file");
                    int fileLen = 0;
                    if (int.TryParse(Console.ReadLine(), out fileLen))
                    {
                        Console.WriteLine("Write filename");
                        logBuilder.AppendLine("Write filename");
                        string filename = Console.ReadLine();
                        FileGenerator.GenerateFile(filename, fileLen);
                    }
                    continue;
                }
                if (input.Equals("comp"))
                {
                    ComplexityTests.Run(myTurtle, SweepAlgorithm);
                    Console.WriteLine("Tests done");
                    continue;
                }
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
                foreach (string p in programs)
                {
                    Console.WriteLine("--------------------------------------------------------");
                    logBuilder.AppendLine("--------------------------------------------------------");
                    Console.WriteLine($"Program logo: \"{p.Trim()}\"");
                    logBuilder.AppendLine($"Program logo: \"{p.Trim()}\"");
                    var segmentList = myTurtle.Parse(p);
                    if (segmentList == null)
                    {
                        Console.WriteLine("Zawiera błąd");
                        logBuilder.AppendLine("Zawiera błąd");
                        continue;
                    }
                    bool intersection = SweepAlgorithm.FindIntersection(segmentList);
                    if (intersection)
                    {
                        Console.WriteLine("Zawiera przecięcie");
                        logBuilder.AppendLine("Zawiera przecięcie");
                    }
                    else
                    {
                        Console.WriteLine("Nie zawiera przecięcia");
                        logBuilder.AppendLine("Nie zawiera przecięcia");
                    }
                }
                using (StreamWriter s = File.AppendText("logs.txt"))
                {
                    s.WriteLine($"Logging date: {DateTime.Now}");
                    s.Write(logBuilder.ToString());
                }
                logBuilder = new StringBuilder();
            }
        }
    }
}

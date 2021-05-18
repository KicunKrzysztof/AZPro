using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogoIntersectionFinder.LogoParser;
using LogoIntersectionFinder.Sweep;
using System.Diagnostics;
using System.IO;

namespace LogoIntersectionFinder.Tests
{
    public interface IComplexityTestInstance
    {
        string Generate(int size);
        string GetName();
        bool ExpectedResult();
        bool ResultIsKnown();
    }
    public class ComplexityTests
    {
        static int delta = 1000;
        static int startSize = 1000;
        static int endSize = 60000;
        static public void Run(Turtle t, SweepAlgorithm a)
        {
            var instances = new List<IComplexityTestInstance>() {
                new Horizontal(),
                new Snail(),
                new HorizontalPlusIntersection(),
                new SnailPlusIntersection(),
                new Saw(),
                new SawPlusIntersection(),
                new Squares(),
                new SquaresPlusIntersection(),
                new Random()};
            List<List<long>> results = new List<List<long>>();
            foreach(IComplexityTestInstance i in instances)
            {
                results.Add(new List<long>());
                for (int size = startSize; size <= endSize; size += delta)
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    string program = i.Generate(size);
                    bool result = a.FindIntersection(t.Parse(program));
                    if (i.ResultIsKnown() == true && result != i.ExpectedResult())
                        throw new Exception("Bad result");
                    stopwatch.Stop();
                    results[results.Count - 1].Add(stopwatch.ElapsedMilliseconds);
                }
            }
            StringBuilder csv = new StringBuilder($"Size");
            foreach (IComplexityTestInstance i in instances)
            {
                csv.Append($", {i.GetName()}");
            }
            csv.Append("\n");
            for (int size = startSize, i = 0; size <= endSize; size += delta, i++)
            {
                csv.Append($"{size}");
                for(int k = 0; k < instances.Count; k++)
                {
                    csv.Append($", {results[k][i].ToString()}");
                }
                csv.Append("\n");
            }
            File.WriteAllText($"result_{DateTime.Now.ToString("yyyyMMdd_HHmm")}.csv", csv.ToString());
        }
    }
}

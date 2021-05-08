using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LogoIntersectionFinder.Sweep;
using LogoIntersectionFinder.Helpers;

namespace TestsProject
{
    [TestClass]
    public class UnitTestSweep
    {
        private Dictionary<int, List<Segment>> IntersectionCases;
        private Dictionary<int, List<Segment>> NotIntersectionCases;
        [TestInitialize]
        public void SetUpData()
        {
            //---###---przypadki z przecięciami:---###---
            IntersectionCases = new Dictionary<int, List<Segment>>();
            IntersectionCases.Add(1, new List<Segment> {
            new Segment(new Point(0, 0), new Point(1, 0), 0),
            new Segment(new Point(0, 1), new Point(1, -1), 2),
            new Segment(new Point(0, 2), new Point(1, 2), 4),
            new Segment(new Point(0, 3), new Point(1, 3), 6)
            });
            IntersectionCases.Add(2, new List<Segment> {
            new Segment(new Point(0, 0), new Point(1, 0), 0),
            new Segment(new Point(1, 0), new Point(2, 0), 1),
            new Segment(new Point(2, 0), new Point(1, 1), 2),
            new Segment(new Point(1, 1), new Point(1, 0), 3),
            new Segment(new Point(1, 0), new Point(1, -2), 4)
            });
            IntersectionCases.Add(3, new List<Segment> {
            new Segment(new Point(0, 0), new Point(5, 0), 0),
            new Segment(new Point(5, 0), new Point(3, 2), 1),
            new Segment(new Point(3, 2), new Point(0, -2), 2)
            });
            IntersectionCases.Add(4, new List<Segment> {
            new Segment(new Point(0, 0), new Point(5, 0), 0),
            new Segment(new Point(5, 0), new Point(-2, 0), 1)
            });

            //---###---przypadki bez przecięć---###---:
            NotIntersectionCases = new Dictionary<int, List<Segment>>();
            NotIntersectionCases.Add(1, new List<Segment> {
            new Segment(new Point(0, 0), new Point(1, 0), 0),
            new Segment(new Point(0, 1), new Point(1, 1), 1),
            new Segment(new Point(0, 2), new Point(1, 2), 2),
            new Segment(new Point(0, 3), new Point(1, 3), 3)
            });
            NotIntersectionCases.Add(2, new List<Segment> {
            new Segment(new Point(0, 0), new Point(1, 0), 0),
            new Segment(new Point(1, 0), new Point(2, 0), 1),
            new Segment(new Point(2, 0), new Point(1, 1), 2),
            new Segment(new Point(1, 1), new Point(1, 0), 3),
            new Segment(new Point(1, 0), new Point(1, -2), 5)
            });
            NotIntersectionCases.Add(3, new List<Segment> {
            new Segment(new Point(0, 0), new Point(5, 0), 0),
            new Segment(new Point(5, 0), new Point(5, 2), 1),
            new Segment(new Point(5, 2), new Point(2, 0), 2),
            new Segment(new Point(2, 0), new Point(0, 2), 3)
            });
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void TestIntersectionNotOccurs(int i)
        {
            var solver = new SweepAlgorithm();
            bool result = solver.FindIntersection(NotIntersectionCases[i]);
            Assert.IsFalse(result);
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        public void TestIntersectionOccurs(int i)
        {
            var solver = new SweepAlgorithm();
            bool result = solver.FindIntersection(IntersectionCases[i]);
            Assert.IsTrue(result);
        }
    }
}

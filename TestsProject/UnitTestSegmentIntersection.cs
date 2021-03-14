using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LogoIntersectionFinder.SweepAlgorithm;
using LogoIntersectionFinder.Helpers;

namespace TestsProject
{
    [TestClass]
    public class UnitTestSegmentIntersection
    {
        [TestMethod]
        [DataRow(0, 0, 0, 5, -1, 1, 1, 1, IntersectionType.Middle)]
        [DataRow(0, 0, 0, 5, 0, 0, 1, 1, IntersectionType.EndToEndP1)]
        [DataRow(0, 0, 0, 5, 0, 1, 1, 1, IntersectionType.OnSegmentP3)]
        [DataRow(0, 0, 0, 5, 0, 5, 0, 10, IntersectionType.EndToEndP2)]
        [DataRow(0, 0, 0, 5, -1, 5, 1, 5, IntersectionType.OnSegmentP2)]
        [DataRow(0, 0, 0, 5, 0, 4, 0, 6, IntersectionType.Overlap)]

        [DataRow(0, 5, 0, 0, 1, 1, -1, 1, IntersectionType.Middle)]
        [DataRow(0, 5, 0, 0, 1, 1, 0, 0, IntersectionType.EndToEndP2)]
        [DataRow(0, 5, 0, 0, 1, 1, 0, 1, IntersectionType.OnSegmentP4)]
        [DataRow(0, 5, 0, 0, 0, 10, 0, 5, IntersectionType.EndToEndP1)]
        [DataRow(0, 5, 0, 0, 1, 5, -1, 5, IntersectionType.OnSegmentP1)]
        [DataRow(0, 5, 0, 0, 0, 4, 0, 6, IntersectionType.Overlap)]
        public void TestIntersectionOccurs(int ax, int ay, int bx, int by, int cx, int cy, int dx, int dy, IntersectionType type)
        {
            Point a = new Point(ax, ay);
            Point b = new Point(bx, by);
            Point c = new Point(cx, cy);
            Point d = new Point(dx, dy);
            Segment ab = new Segment(a, b);
            Segment cd = new Segment(c, d);
            IntersectionType intersection = SegmentIntersectionChecker.Check(ab, cd);
            Assert.IsTrue(intersection == type);
        }

        [TestMethod]
        [DataRow(0, 0, 0, 5, -1, -1, 1, -1, IntersectionType.None)]
        [DataRow(0, 0, 0, 5, 0, 6, 0, 8, IntersectionType.None)]
        [DataRow(0, 5, 0, 0, 1, -1, -1, -1, IntersectionType.None)]
        [DataRow(0, 5, 0, 0, 0, 8, 0, 6, IntersectionType.None)]
        public void TestIntersectionNotOccurs(int ax, int ay, int bx, int by, int cx, int cy, int dx, int dy, IntersectionType type)
        {
            Point a = new Point(ax, ay);
            Point b = new Point(bx, by);
            Point c = new Point(cx, cy);
            Point d = new Point(dx, dy);
            Segment ab = new Segment(a, b);
            Segment cd = new Segment(c, d);
            IntersectionType intersection = SegmentIntersectionChecker.Check(ab, cd);
            Assert.IsTrue(intersection == type);
        }
    }
}

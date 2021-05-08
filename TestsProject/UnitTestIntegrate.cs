using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogoIntersectionFinder.LogoParser;
using LogoIntersectionFinder.Sweep;

namespace TestsProject
{
    [TestClass]
    public class UnitTestIntegrate
    {
        [TestMethod]
        [DataRow("fd 100 bk 100")]
        [DataRow("fd 100 pu bk 50 lt 90 fd 50 lt 180 pd fd 100")]
        [DataRow("fd 100 pu bk 50 lt 90 fd 50 lt 180 pd fd 50 fd 50")]
        [DataRow("fd 100 pu bk 50 lt 90 fd 50 lt 180 pd fd 50 lt 45 fd 50")]
        public void TestMethodIntersectionOccurs(string program)
        {
            Turtle myTurtle = new Turtle();
            SweepAlgorithm SweepAlgorithm = new SweepAlgorithm();
            var segmentList = myTurtle.Parse(program);
            bool intersection = SweepAlgorithm.FindIntersection(segmentList);
            Assert.IsTrue(intersection);
        }
        [TestMethod]
        [DataRow("fd 100 rt 90 fd 100 rt 90 fd 100 rt 90 fd 100 rt 90")]
        [DataRow("fd 100 pu bk 50 lt 90 fd 50 lt 180 pd fd 50")]
        [DataRow("fd 100 pu bk 50 rt 45 fd 50 lt 180 pd fd 50 lt 90 fd 50")]
        [DataRow("fd 100 pu bk 50 lt 90 fd 50 lt 180 pd fd 50 pu lt 45 fd 100 lt 45 pd fd 25 pu bk 25 rt 45 bk 100 rt 45 pd fd 50")]
        public void TestMethodIntersectionNotOccurs(string program)
        {
            Turtle myTurtle = new Turtle();
            SweepAlgorithm SweepAlgorithm = new SweepAlgorithm();
            var segmentList = myTurtle.Parse(program);
            bool intersection = SweepAlgorithm.FindIntersection(segmentList);
            Assert.IsFalse(intersection);
        }
    }
}

using System;
using LogoIntersectionFinder.Helpers;

namespace LogoIntersectionFinder.Sweep
{
    public class SegmentIntersectionChecker
    {
        public static IntersectionType Check(Segment ab, Segment cd)
        {
            var val1 = InnerCheck(ab, cd);
            var val2 = InnerCheck(cd, ab);
            if (val1 == IntersectionType.Overlap || val2 == IntersectionType.Overlap)
                return IntersectionType.Overlap;
            else if (val1 == IntersectionType.EndToEndP1)
                return IntersectionType.EndToEndP1;
            else if (val1 == IntersectionType.EndToEndP2)
                return IntersectionType.EndToEndP2;
            else if (val1 == IntersectionType.OnSegmentP3)
                return IntersectionType.OnSegmentP3;
            else if (val1 == IntersectionType.OnSegmentP4)
                return IntersectionType.OnSegmentP4;
            else if (val2 == IntersectionType.OnSegmentP3)
                return IntersectionType.OnSegmentP1;
            else if (val2 == IntersectionType.OnSegmentP4)
                return IntersectionType.OnSegmentP2;
            else if (val1 == IntersectionType.Middle && val2 == IntersectionType.Middle)
                return IntersectionType.Middle;
            else
                return IntersectionType.None;
        }
        private static IntersectionType InnerCheck(Segment ab, Segment cd)
        {
            Vector vab = new Vector(ab.Point2.X - ab.Point1.X, ab.Point2.Y - ab.Point1.Y);
            Vector vac = new Vector(cd.Point1.X - ab.Point1.X, cd.Point1.Y - ab.Point1.Y);
            Vector vad = new Vector(cd.Point2.X - ab.Point1.X, cd.Point2.Y - ab.Point1.Y);
            long val1 = vab.CrossProduct(vac);
            long val2 = vab.CrossProduct(vad);
            int val3 = SaveMultiply(val1, val2);
            if (val3 == 0)
            {
                if (val1 == 0 && val2 == 0 && OneLineOverlapCheck(ab, cd))
                    return IntersectionType.Overlap;
                else if (EndToEndCheck(ab.Point1, cd.Point1, cd.Point2))
                    return IntersectionType.EndToEndP1;
                else if (EndToEndCheck(ab.Point2, cd.Point1, cd.Point2))
                    return IntersectionType.EndToEndP2;
                else if (val1 == 0 && PointsOnOneLineCheck(ab.Point1, ab.Point2, cd.Point1))
                    return IntersectionType.OnSegmentP3;
                else if (val2 == 0 && PointsOnOneLineCheck(ab.Point1, ab.Point2, cd.Point2))
                    return IntersectionType.OnSegmentP4;
                else
                    return IntersectionType.None;
            }
            if (val3 < 0)
                return IntersectionType.Middle;
            return IntersectionType.None;
        }

        private static int SaveMultiply(long val1, long val2)
        {
            if (val1 == 0 || val2 == 0)
                return 0;
            if (val1 < 0 && val2 < 0 || val1 > 0 && val2 > 0)
                return 1;
            return -1;
        }

        private static bool PointsOnOneLineCheck(Point p1, Point p2, Point p3)
        {
            return Math.Min(p1.X, p2.X) <= p3.X && p3.X <= Math.Max(p1.X, p2.X)
                && Math.Min(p1.Y, p2.Y) <= p3.Y && p3.Y <= Math.Max(p1.Y, p2.Y);
        }
        private static bool OneLineOverlapCheck(Segment ab, Segment cd)
        {
            return (Math.Min(ab.Point1.X, ab.Point2.X) < cd.Point1.X && cd.Point1.X < Math.Max(ab.Point1.X, ab.Point2.X)
                && Math.Min(ab.Point1.Y, ab.Point2.Y) <= cd.Point1.Y && cd.Point1.Y <= Math.Max(ab.Point1.Y, ab.Point2.Y))
                || (Math.Min(ab.Point1.X, ab.Point2.X) <= cd.Point1.X && cd.Point1.X <= Math.Max(ab.Point1.X, ab.Point2.X)
                && Math.Min(ab.Point1.Y, ab.Point2.Y) < cd.Point1.Y && cd.Point1.Y < Math.Max(ab.Point1.Y, ab.Point2.Y))
                || (Math.Min(ab.Point1.X, ab.Point2.X) < cd.Point2.X && cd.Point2.X < Math.Max(ab.Point1.X, ab.Point2.X)
                && Math.Min(ab.Point1.Y, ab.Point2.Y) <= cd.Point2.Y && cd.Point2.Y <= Math.Max(ab.Point1.Y, ab.Point2.Y))
                || (Math.Min(ab.Point1.X, ab.Point2.X) <= cd.Point2.X && cd.Point2.X <= Math.Max(ab.Point1.X, ab.Point2.X)
                && Math.Min(ab.Point1.Y, ab.Point2.Y) < cd.Point2.Y && cd.Point2.Y < Math.Max(ab.Point1.Y, ab.Point2.Y));
        }
        private static bool EndToEndCheck(Point p1, Point p2, Point p3)
        {
            return p1.Equals(p2) || p1.Equals(p3);
        }
    }
}

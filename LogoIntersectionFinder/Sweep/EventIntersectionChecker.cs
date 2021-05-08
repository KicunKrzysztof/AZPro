using System.Collections.Generic;
using LogoIntersectionFinder.Helpers;

namespace LogoIntersectionFinder.Sweep
{
    public class EventIntersectionChecker
    {
        private Point p;
        private List<Segment> VirtualSegments;
        private bool twoMiddleSegments;
        public EventIntersectionChecker(Event e)
        {
            p = e.P;
            VirtualSegments = new List<Segment>();
            bool middleFlag = false;
            for (int i = 0; i < e.Segments.Count; i++)
            {
                if(e.Segments[i].Type == EventSegmentType.OnMiddle)
                {
                    VirtualSegments.Add(e.Segments[i].Segment);
                    if (middleFlag)
                    {
                        twoMiddleSegments = true;
                        break;
                    }
                    middleFlag = true;
                }
                else
                {
                    for (int j = i + 1; j < e.Segments.Count; j++)
                    {
                        if(e.Segments[j].Segment.Id == e.Segments[i].Segment.Id + 1 || e.Segments[j].Segment.Id == e.Segments[i].Segment.Id - 1)
                        {
                            Point p1 = e.Segments[i].Segment.Point1 == e.P ? e.Segments[i].Segment.Point2 : e.Segments[i].Segment.Point1;
                            Point p2 = e.Segments[j].Segment.Point1 == e.P ? e.Segments[j].Segment.Point2 : e.Segments[j].Segment.Point1;
                            VirtualSegments.Add(new Segment(p1, p2));
                        }
                    }
                }
            }
        }

        public bool Check()
        {
            if (twoMiddleSegments)
                return true;
            for(int i = 0; i < VirtualSegments.Count; i++)
            {
                for (int j = i + 1; j < VirtualSegments.Count; j++)
                {
                    if (VirtualIntersection(VirtualSegments[i], VirtualSegments[j]))
                        return true;
                }
            }
            return false;
        }

        private bool VirtualIntersection(Segment segment1, Segment segment2)
        {
            Point p1 = new Point(segment1.Point1.X - p.X, segment1.Point1.Y - p.Y);
            Point p2 = new Point(segment1.Point2.X - p.X, segment1.Point2.Y - p.Y);
            Point p3 = new Point(segment2.Point1.X - p.X, segment2.Point1.Y - p.Y);
            Point p4 = new Point(segment2.Point2.X - p.X, segment2.Point2.Y - p.Y);
            int val1 = p1.X * p3.Y - p3.X * p1.Y;
            int val2 = p2.X * p3.Y - p3.X * p2.Y;
            if (val1 * val2 > 0)
                return false;
            val1 = p1.X * p4.Y - p4.X * p1.Y;
            val2 = p2.X * p4.Y - p4.X * p2.Y;
            if (val1 * val2 > 0)
                return false;
            return true;
        }
    }
}

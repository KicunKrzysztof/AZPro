using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogoIntersectionFinder.Helpers;

namespace LogoIntersectionFinder.Sweep
{
    public class Event :IComparable<Event>
    {
        public Point P { get; set; }
        public List<EventSegment> Segments { get; set; }
        public Event(Point p, Segment s)
        {
            P = p;
            Segments = new List<EventSegment> {new EventSegment(s, EventSegmentType.OnEnd)};
        }
        public Event(Point p, Segment s1, EventSegmentType type1, Segment s2, EventSegmentType type2)
        {
            P = p;
            Segments = new List<EventSegment> { new EventSegment(s1, type1), new EventSegment(s2, type2) };
        }
        public void AddSegment(Segment s, EventSegmentType type)
        {
            if (Segments.Where(es => es.Segment == s).Count() > 0)
                return;
            Segments.Add(new EventSegment(s, type));
        }

        public int CompareTo(Event other)
        {
            if (P < other.P)
                return -1;
            else if (P > other.P)
                return 1;
            else
                return 0;
        }
    }
}

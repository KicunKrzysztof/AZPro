using LogoIntersectionFinder.Helpers;

namespace LogoIntersectionFinder.Sweep
{
    public class EventSegment
    {
        public Segment Segment { get; set; }
        public EventSegmentType Type {get; set;}

        public EventSegment(Segment s, EventSegmentType type)
        {
            Segment = s;
            Type = type;
        }
    }
}

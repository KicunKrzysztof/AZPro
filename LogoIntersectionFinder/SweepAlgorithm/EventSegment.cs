using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogoIntersectionFinder.Helpers;

namespace LogoIntersectionFinder.SweepAlgorithm
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

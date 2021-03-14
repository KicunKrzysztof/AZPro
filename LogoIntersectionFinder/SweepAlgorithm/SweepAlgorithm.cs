using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogoIntersectionFinder.Helpers;

namespace LogoIntersectionFinder.SweepAlgorithm
{
    public class SweepAlgorithm
    {
        private SortedList<Event, Event> eventsQueue;
        private SortedList<Segment, Segment> sweep;
        private IList<Segment> sweepList;
        public bool FindIntersection(List<Segment> segments)
        {
            InitializeEventList(segments);
            sweep = new SortedList<Segment, Segment>();
            sweepList = sweep.Values;
            while(eventsQueue.Count != 0)
            {
                Event e = eventsQueue.Keys[0];
                for (int i = 0; i < e.Segments.Count; i++)// EventSegment seg in e.Segments.Where(seg => seg.Type == EventSegmentType.OnEnd))//odcinki OnMiddle już są i zostają na miotle
                {
                    if (e.Segments[i].Type == EventSegmentType.OnMiddle)
                        continue;
                    Segment seg = e.Segments[i].Segment;
                    if (seg.GetLeftPoint() == e.P)//lewy koniec, dodanie do miotły
                    {
                        sweep.Add(seg, seg);
                        int idx = sweep.IndexOfKey(seg);
                        if (idx > 0 && CheckIntersection(sweepList[idx - 1], sweepList[idx]))
                        {
                            return true;
                        }
                        if (idx + 1 < sweepList.Count && CheckIntersection(sweepList[idx], sweepList[idx + 1]))
                        {
                            return true;
                        }
                    }
                    else//prawy koniec odcinka (usunięcie z miotły)
                    {
                        int idx = sweep.IndexOfKey(seg);
                        sweep.RemoveAt(idx);
                        if (idx > 0 && idx < sweepList.Count && CheckIntersection(sweepList[idx - 1], sweepList[idx]))
                        {
                            return true;
                        }
                    }
                }
                if (CheckComplexEventIntersection(e))
                    return true;
                eventsQueue.RemoveAt(0);
            }
            return false;
        }

        private bool CheckComplexEventIntersection(Event e)
        {
            EventIntersectionChecker checker = new EventIntersectionChecker(e);
            return checker.Check();
        }

        private bool CheckIntersection(Segment segment1, Segment segment2)
        {
            var intersection = SegmentIntersectionChecker.Check(segment1, segment2);
            if (intersection <= IntersectionType.Middle)
                return true;
            else if (intersection != IntersectionType.None)
            {
                AddEvent(segment1, segment2, intersection);
            }
            return false;
        }

        private void AddEvent(Segment segment1, Segment segment2, IntersectionType intersection)
        {
            Event newEvent = new Event(new Point(0, 0), segment1);
            EventSegmentType s1EventType = EventSegmentType.OnEnd, s2EventType = EventSegmentType.OnEnd;
            Point p = new Point(0, 0);
            switch (intersection)
            {
                case IntersectionType.EndToEndP1:
                    s1EventType = EventSegmentType.OnEnd;
                    s2EventType = EventSegmentType.OnEnd;
                    p = segment1.Point1;
                    break;
                case IntersectionType.EndToEndP2:
                    s1EventType = EventSegmentType.OnEnd;
                    s2EventType = EventSegmentType.OnEnd;
                    p = segment1.Point2;
                    break;
                case IntersectionType.OnSegmentP1:
                    s1EventType = EventSegmentType.OnEnd;
                    s2EventType = EventSegmentType.OnMiddle;
                    p = segment1.Point1;
                    break;
                case IntersectionType.OnSegmentP2:
                    s1EventType = EventSegmentType.OnEnd;
                    s2EventType = EventSegmentType.OnMiddle;
                    p = segment1.Point2;
                    break;
                case IntersectionType.OnSegmentP3:
                    s1EventType = EventSegmentType.OnMiddle;
                    s2EventType = EventSegmentType.OnEnd;
                    p = segment2.Point1;
                    break;
                case IntersectionType.OnSegmentP4:
                    s1EventType = EventSegmentType.OnMiddle;
                    s2EventType = EventSegmentType.OnEnd;
                    p = segment2.Point2;
                    break;
                default:
                    throw new Exception();
            }
            newEvent = new Event(p, segment1, s1EventType, segment2, s2EventType);
            Event existingEvent;
            if (eventsQueue.TryGetValue(newEvent, out existingEvent))
            {
                existingEvent.AddSegment(segment1, s1EventType);
                existingEvent.AddSegment(segment2, s2EventType);
            }
            else
            {
                eventsQueue.Add(newEvent, newEvent);
            }
        }

        private void InitializeEventList(List<Segment> segments)
        {
            eventsQueue = new SortedList<Event, Event>();
            foreach(Segment s in segments)
            {
                Point entryPoint = s.GetLeftPoint();
                Event newEvent = new Event(entryPoint, s), existingEvent;
                if (eventsQueue.TryGetValue(newEvent, out existingEvent))
                {
                    existingEvent.AddSegment(s, EventSegmentType.OnEnd);
                }
                else
                {
                    eventsQueue.Add(newEvent, newEvent);
                }
            }
        }
    }
}
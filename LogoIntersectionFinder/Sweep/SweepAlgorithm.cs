using System;
using System.Collections.Generic;
using LogoIntersectionFinder.Helpers;

namespace LogoIntersectionFinder.Sweep
{
    public class SweepAlgorithm
    {
        private AVL<Event> eventsQueue;
        private AVL<Segment> sweep;
        public bool FindIntersection(List<Segment> segments)
        {
            InitializeEventList(segments);
            sweep = new AVL<Segment>();
            while(!eventsQueue.IsEmpty())
            {
                Event e = eventsQueue.GetMin();
                for (int i = 0; i < e.Segments.Count; i++)// EventSegment seg in e.Segments.Where(seg => seg.Type == EventSegmentType.OnEnd))//odcinki OnMiddle już są i zostają na miotle
                {
                    if (e.Segments[i].Type == EventSegmentType.OnMiddle)
                        continue;
                    Segment seg = e.Segments[i].Segment;
                    if (seg.GetLeftPoint() == e.P)//lewy koniec, dodanie do miotły
                    {
                        if(sweep.Find(seg) != null)//odcinki nakładające się
                            return true;
                        sweep.Add(seg);
                        Segment prev = sweep.FindPrev(seg);
                        Segment next = sweep.FindNext(seg);
                        if (prev != null && CheckIntersection(prev, seg))
                            return true;
                        if (next != null && CheckIntersection(seg, next))
                            return true;
                    }
                    else//prawy koniec odcinka (usunięcie z miotły)
                    {
                        Segment prev = sweep.FindPrev(seg);
                        Segment next = sweep.FindNext(seg);
                        sweep.Delete(seg);
                        if (prev != null && next != null && CheckIntersection(prev, next))
                            return true;
                    }
                }
                if (CheckComplexEventIntersection(e))
                    return true;
                eventsQueue.Delete(e);
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
            Event existingEvent = eventsQueue.Find(newEvent);
            if (existingEvent != null)
            {
                existingEvent.AddSegment(segment1, s1EventType);
                existingEvent.AddSegment(segment2, s2EventType);
            }
            else
            {
                eventsQueue.Add(newEvent);
            }
        }

        private void InitializeEventList(List<Segment> segments)
        {
            eventsQueue = new AVL<Event>();
            foreach(Segment s in segments)
            {
                Point leftPoint = s.GetLeftPoint();
                Event newEvent = new Event(leftPoint, s), existingEvent;
                existingEvent = eventsQueue.Find(newEvent);
                if (existingEvent != null)
                {
                    existingEvent.AddSegment(s, EventSegmentType.OnEnd);
                }
                else
                {
                    eventsQueue.Add(newEvent);
                }

                Point rightPoint = s.GetRightPoint();
                newEvent = new Event(rightPoint, s);
                existingEvent = eventsQueue.Find(newEvent);
                if (existingEvent != null)
                {
                    existingEvent.AddSegment(s, EventSegmentType.OnEnd);
                }
                else
                {
                    eventsQueue.Add(newEvent);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoIntersectionFinder.Helpers
{
    public class Segment : IComparable<Segment>
    {
        public int Id { get; set; }
        public Point Point1 { set; get;}
        public Point Point2 { set; get; }
        public Segment (Point p1, Point p2)
        {
            Point1 = p1;
            Point2 = p2;
        }
        public Segment(Point p1, Point p2, int id)
        {
            Point1 = p1;
            Point2 = p2;
            Id = id;
        }
        public Point GetLeftPoint()
        {
            return Point1 < Point2 ? Point1 : Point2;
        }
        public Point GetRightPoint()
        {
            return Point1 < Point2 ? Point2 : Point1;
        }

        public int CompareTo(Segment other) //sortowanie na miotle
        {
            if ((Point1 == other.Point1 && Point2 == other.Point2) || (Point1 == other.Point2 && Point2 == other.Point1))
                return 0;
            var p1 = GetLeftPoint();
            var p2 = other.GetLeftPoint();
            if(p1.X == p2.X)
            {
                if (p1.Y == p2.Y)
                    return IsPointUpper(this, other.GetRightPoint()) ? -1 : 1;
                return p1.Y < p2.Y ? -1 : 1;
            }
            else
            {
                if(p1.X < p2.X)
                {
                    if (IsPointUpper(this, p2))
                    {
                        return -1;
                    }
                    else
                        return 1;
                }
                else
                {
                    if (IsPointUpper(other, p1))
                    {
                        return 1;
                    }
                    else
                        return -1;
                }
            }
        }
        public bool IsPointUpper(Segment s, Point p)
        {
            Point p_left = s.GetLeftPoint();
            Point p_right = s.Point1 == p_left ? s.Point2 : s.Point1;
            Point p1 = new Point(p_right.X - p_left.X, p_right.Y - p_left.Y);
            Point p2 = new Point(p.X - p_left.X, p.Y - p_left.Y);
            return p1.X * p2.Y - p2.X * p1.Y > 0 ? true : false;
        }
        public static bool operator ==(Segment p1, Segment p2)
        {
            return (p1.Point1 == p2.Point1 && p1.Point2 == p2.Point2) || (p1.Point1 == p2.Point2 && p1.Point2 == p2.Point1);
        }
        public static bool operator !=(Segment s1, Segment s2)
        {
            return !(s1 == s2);
        }
    }
}

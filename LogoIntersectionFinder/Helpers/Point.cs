namespace LogoIntersectionFinder.Helpers
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override bool Equals(object obj)
        {
            Point p = (Point)obj;
            return X == p.X && Y == p.Y;
        }
        public static bool operator >(Point p1, Point p2)
        {
            if(p1.X < p2.X)
                return false;
            else if (p1.X > p2.X)
                return true;
            else if (p1.Y < p2.Y)
                return false;
            else if (p1.Y > p2.Y)
                return true;
            else
                return false;
        }
        public static bool operator <(Point p1, Point p2)
        {
            return p2 > p1;
        }
        public static bool operator ==(Point p1, Point p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }
        public static bool operator !=(Point p1, Point p2)
        {
            return !(p1 == p2);
        }
    }
}

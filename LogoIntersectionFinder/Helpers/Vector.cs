namespace LogoIntersectionFinder.Helpers
{
    public class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int CrossProduct(Vector v)
        {
            return X * v.Y - v.X * Y;
        }
    }
}

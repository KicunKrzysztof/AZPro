namespace LogoIntersectionFinder.Helpers
{
    public class Vector
    {
        public long X { get; set; }
        public long Y { get; set; }
        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }
        public long CrossProduct(Vector v)
        {
            return X * v.Y - v.X * Y;
        }
    }
}

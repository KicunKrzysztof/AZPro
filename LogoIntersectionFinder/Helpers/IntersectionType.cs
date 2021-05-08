namespace LogoIntersectionFinder.Helpers
{
    //dwa odcinki to 4 punkty: P1, P2, P3, P4
    public enum IntersectionType
    {
        Overlap,
        Middle,
        EndToEndP1, //zetknięcie na końcu P1 (oraz P3 lub P4)
        EndToEndP2, //zetknięcie na końcu P2 (oraz P3 lub P4)
        OnSegmentP1, //zetknięcie na końcu P1
        OnSegmentP2, //zetknięcie na końcu P2
        OnSegmentP3, //zetknięcie na końcu P3
        OnSegmentP4, //zetknięcie na końcu P4
        None
    }
}

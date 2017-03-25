namespace CinemaRest.Routing
{
    public class RouteSegment
    {
        public RouteSegmentMatcherKinds Kind { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
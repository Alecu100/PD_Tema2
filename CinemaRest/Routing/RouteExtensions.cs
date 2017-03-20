namespace CinemaRest.Routing
{
    public static class RouteExtensions
    {
        public static Route AddLiteral(this Route route, string literal)
        {
            route.Add(new RouteSegment {Kind = RouteSegmentMatcherKinds.Literal, Name = literal});

            return route;
        }

        public static Route AddDefaultAction(this Route route, string defaultActionName)
        {
            route.Add(new RouteSegment {Kind = RouteSegmentMatcherKinds.Literal, Name = defaultActionName});

            return route;
        }

        public static Route AddAction(this Route route)
        {
            route.Add(new RouteSegment {Kind = RouteSegmentMatcherKinds.Literal});

            return route;
        }

        public static Route AddDefaultController(this Route route, string defaultControllerName)
        {
            route.Add(new RouteSegment {Kind = RouteSegmentMatcherKinds.Controller, Name = defaultControllerName});

            return route;
        }

        public static Route AddController(this Route route)
        {
            route.Add(new RouteSegment {Kind = RouteSegmentMatcherKinds.Controller});

            return route;
        }

        public static Route 
    }
}
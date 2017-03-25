namespace CinemaRest.Routing
{
    public static class RouteExtensions
    {
        public static Route AddLiteral(this Route route, string literal)
        {
            route.Add(new RouteSegment {Kind = RouteSegmentMatcherKinds.Literal, Name = literal});

            return route;
        }

        public static Route AddDefaultAction(this Route route, string defaultActionName, string defaultLiteralName)
        {
            route.Add(new RouteSegment
            {
                Kind = RouteSegmentMatcherKinds.Literal,
                Value = defaultActionName,
                Name = defaultLiteralName
            });

            return route;
        }

        public static Route AddAction(this Route route)
        {
            route.Add(new RouteSegment {Kind = RouteSegmentMatcherKinds.Literal});

            return route;
        }

        public static Route AddDefaultController(this Route route, string defaultControllerName,
            string defaultLiteralName)
        {
            route.Add(new RouteSegment
            {
                Kind = RouteSegmentMatcherKinds.Controller,
                Value = defaultControllerName,
                Name = defaultLiteralName
            });

            return route;
        }

        public static Route AddController(this Route route)
        {
            route.Add(new RouteSegment {Kind = RouteSegmentMatcherKinds.Controller});

            return route;
        }

        public static Route AddParameter(this Route route, string parameterName)
        {
            route.Add(new RouteSegment {Kind = RouteSegmentMatcherKinds.Parameter, Name = parameterName});

            return route;
        }
    }
}
using System.Collections.Generic;

namespace CinemaRest.Routing
{
    public class RouteControllerMatchResult
    {
        public bool IsMatch { get; set; }

        public string Method { get; set; }

        public List<RouteControllerMatchParameter> MethodParameters { get; set; } =
            new List<RouteControllerMatchParameter>();
    }
}
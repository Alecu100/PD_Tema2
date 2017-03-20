using System.Collections.Generic;
using System.Reflection;

namespace CinemaRest.Routing
{
    public class RouteControllerMatchResult
    {
        public bool IsMatch { get; set; }

        public MethodInfo Method { get; set; }

        public List<RouteControllerMatchParameter> MethodParameters { get; set; } =
            new List<RouteControllerMatchParameter>();
    }
}
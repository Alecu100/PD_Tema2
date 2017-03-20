using System.Collections.Generic;

namespace CinemaRest.Routing
{
    public class RouteCollection : List<Route>
    {
        public Route AddNewRoute()
        {
            var route = new Route();

            Add(route);

            return route;
        }
    }
}
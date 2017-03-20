using System;
using System.Collections.Generic;
using CinemaRest.Controllers;

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

        public RouteControllerMatchResult MatchController(Type controller, string relativeUrl)
        {
            foreach (var route in this)
            {
                var routeControllerMatchResult = route.MatchAgainstController(controller, relativeUrl);

                if (routeControllerMatchResult.IsMatch)
                    return routeControllerMatchResult;
            }

            return new RouteControllerMatchResult {IsMatch = false};
        }
    }
}
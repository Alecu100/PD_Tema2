using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using CinemaRest.Controllers;

namespace CinemaRest.Routing
{
    public class Route : List<RouteSegment>
    {
        public RouteControllerMatchResult MatchAgainstController(string relativeUrl, BaseController controller)
        {
            var segments = relativeUrl.Split('\\', '/');

            var controllerMatchResult = new RouteControllerMatchResult();

            var foundMethods = new List<MethodInfo>();

            if (segments.Length != Count)
            {
                controllerMatchResult.IsMatch = false;
                return controllerMatchResult;
            }

            for (var i = 0; i < Count; i++)
            {
                var currentRouteSegment = this[i];

                if (currentRouteSegment.Kind == RouteSegmentMatcherKinds.Literal &&
                    !string.IsNullOrEmpty(currentRouteSegment.Name))
                {
                    if (
                        string.Compare(currentRouteSegment.Name, segments[i],
                            StringComparison.InvariantCultureIgnoreCase) != 0)
                    {
                        controllerMatchResult.IsMatch = false;
                        return controllerMatchResult;
                    }
                }
                else if (currentRouteSegment.Kind == RouteSegmentMatcherKinds.Controller)
                {
                    var controllerName = controller.GetType().Name;

                    if (string.Compare(currentRouteSegment.Name, controllerName,
                            StringComparison.InvariantCultureIgnoreCase) != 0)
                    {
                        controllerMatchResult.IsMatch = false;
                        return controllerMatchResult;
                    }
                }
                else if (currentRouteSegment.Kind == RouteSegmentMatcherKinds.Action)
                {
                    var methodInfos = controller.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic);

                    foreach (var methodInfo in methodInfos)
                        if (string.Compare(methodInfo.Name, segments[i], StringComparison.InvariantCultureIgnoreCase) ==
                            0)
                            foundMethods.Add(methodInfo);
                } else if (currentRouteSegment.Kind == RouteSegmentMatcherKinds.Ignore)
                {
                    continue;
                } else if (currentRouteSegment.Kind == RouteSegmentMatcherKinds.Parameter)
                {
                    
                }
            }

            return controllerMatchResult;
        }
    }
}
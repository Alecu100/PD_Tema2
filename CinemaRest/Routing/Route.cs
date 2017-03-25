using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CinemaRest.Routing
{
    public class Route : List<RouteSegment>
    {
        public RouteControllerMatchResult MatchAgainstController(Type controller, string relativeUrl)
        {
            var segments = relativeUrl.Split('\\', '/').Where(segment => !string.IsNullOrWhiteSpace(segment)).ToArray();

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
                    if (string.IsNullOrEmpty(currentRouteSegment.Name))
                    {
                        var controllerName = controller.Name.ToLower();

                        var controllerNameToCompare = segments[i];

                        if (controllerName.EndsWith("controller", StringComparison.InvariantCultureIgnoreCase))
                            controllerName = controllerName.TrimEnd("controller".ToCharArray());

                        if (string.Compare(controllerNameToCompare, controllerName,
                                StringComparison.InvariantCultureIgnoreCase) != 0)
                        {
                            controllerMatchResult.IsMatch = false;
                            return controllerMatchResult;
                        }
                    }
                    else
                    {
                        var controllerName = controller.Name.ToLower();

                        var controllerNameToCompare = currentRouteSegment.Value;

                        if (controllerName.EndsWith("controller", StringComparison.InvariantCultureIgnoreCase))
                            controllerName = controllerName.TrimEnd("controller".ToCharArray());

                        if (string.Compare(controllerNameToCompare, controllerName,
                                StringComparison.InvariantCultureIgnoreCase) != 0)
                        {
                            controllerMatchResult.IsMatch = false;
                            return controllerMatchResult;
                        }

                        if (
                            string.Compare(currentRouteSegment.Name, segments[i],
                                StringComparison.InvariantCultureIgnoreCase) != 0)
                        {
                            controllerMatchResult.IsMatch = false;
                            return controllerMatchResult;
                        }
                    }
                }
                else if (currentRouteSegment.Kind == RouteSegmentMatcherKinds.Action)
                {
                    var methodInfos =
                        controller.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                    if (string.IsNullOrEmpty(currentRouteSegment.Name))
                    {
                        var methodName = currentRouteSegment.Name ?? segments[i];

                        foreach (var methodInfo in methodInfos)
                            if (
                                string.Compare(methodInfo.Name, methodName, StringComparison.InvariantCultureIgnoreCase) ==
                                0)
                                foundMethods.Add(methodInfo);
                    }
                    else
                    {
                        if (
                            string.Compare(currentRouteSegment.Name, segments[i],
                                StringComparison.InvariantCultureIgnoreCase) != 0)
                        {
                            controllerMatchResult.IsMatch = false;
                            return controllerMatchResult;
                        }


                        foreach (var methodInfo in methodInfos)
                            if (
                                string.Compare(methodInfo.Name, currentRouteSegment.Value,
                                    StringComparison.InvariantCultureIgnoreCase) ==
                                0)
                                foundMethods.Add(methodInfo);
                    }
                }
                else if (currentRouteSegment.Kind == RouteSegmentMatcherKinds.Ignore)
                {
                }
                else if (currentRouteSegment.Kind == RouteSegmentMatcherKinds.Parameter)
                {
                    controllerMatchResult.MethodParameters.Add(new RouteControllerMatchParameter
                    {
                        Name = currentRouteSegment.Name,
                        Value = segments[i]
                    });
                }
            }

            foreach (var foundMethod in foundMethods)
            {
                var parameterInfos = foundMethod.GetParameters();

                if (parameterInfos.Length != controllerMatchResult.MethodParameters.Count)
                    continue;

                var hasAllParameters = true;

                foreach (var parameterInfo in parameterInfos)
                    if (
                        controllerMatchResult.MethodParameters.All(
                            methodParameter =>
                                string.Compare(methodParameter.Name, parameterInfo.Name,
                                    StringComparison.InvariantCultureIgnoreCase) != 0))
                    {
                        hasAllParameters = false;
                        break;
                    }

                if (hasAllParameters)
                {
                    controllerMatchResult.Method = foundMethod;
                    break;
                }
            }

            if (controllerMatchResult.Method == null)
            {
                controllerMatchResult.IsMatch = false;
                return controllerMatchResult;
            }

            controllerMatchResult.IsMatch = true;

            return controllerMatchResult;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using CinemaRest.Controllers;
using CinemaRest.Helpers;
using CinemaRest.Routing;

namespace CinemaRest.Server
{
    internal class MasterServer
    {
        private readonly List<Type> _controllers = new List<Type>();
        private readonly RouteCollection _routes = new RouteCollection();

        public void Start()
        {
            InitializeControllers();

            InitializeRoutes();

            var web = new HttpListener();

            web.Prefixes.Add(Constants.HostUrl);

            Console.WriteLine("Listening..");

            web.Start();
            while (true)
            {
                var context = web.GetContext();
                Console.WriteLine("Rq: " + context.Request.Url);

                if (TryToHandleRequestByController(context))
                    return;

                WriteDefaultError(context);
            }
            web.Stop();
        }

        private void InitializeRoutes()
        {
            _routes.AddNewRoute().AddController().AddDefaultAction("GetAll", "all");
        }

        private static void WriteDefaultError(HttpListenerContext context)
        {
            var response = context.Response;
            var responseString = "Error happened";

            var buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;

            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            Console.WriteLine(output);
            output.Close();
        }

        private bool TryToHandleRequestByController(HttpListenerContext context)
        {
            var urlPathAndQuery = context.Request.Url.PathAndQuery;

            foreach (var controller in _controllers)
            {
                var routeControllerMatchResult = _routes.MatchController(controller, urlPathAndQuery);

                if (routeControllerMatchResult.IsMatch)
                {
                    var controllerInstance = Activator.CreateInstance(controller, BindingFlags.CreateInstance, context);

                    var targetMethod = routeControllerMatchResult.Method;

                    var targetParameters = targetMethod.GetParameters();

                    var arguments = new List<object>(targetParameters.Length);

                    for (var i = 0; i < targetParameters.Length; i++)
                    {
                        var parameterValue = routeControllerMatchResult.MethodParameters.First(
                            param =>
                                string.Equals(param.Name, targetParameters[i].Name,
                                    StringComparison.InvariantCultureIgnoreCase));

                        arguments[i] = parameterValue.Value;
                    }

                    targetMethod.Invoke(controllerInstance, arguments.ToArray());

                    return true;
                }
            }

            return false;
        }

        private void InitializeControllers()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    var hasControllerBaseType = false;
                    var baseType = type.BaseType;

                    while (true)
                    {
                        if (baseType != null && baseType == typeof(Controller))
                        {
                            hasControllerBaseType = true;
                            break;
                        }

                        if (baseType == null)
                            break;

                        baseType = baseType.BaseType;
                    }

                    if (hasControllerBaseType && type != typeof(Controller))
                        _controllers.Add(type);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using CinemaRest.Controllers;
using CinemaRest.Controllers.ActionResults;
using CinemaRest.Controllers.Authorization;
using CinemaRest.Helpers;
using CinemaRest.Routing;

namespace CinemaRest.Server
{
    internal class MasterServer
    {
        private readonly List<Type> _controllers = new List<Type>();

        private readonly RouteCollection _routes = new RouteCollection();

        private HttpListener _httpListener;

        private Thread _mainThread;

        private volatile bool _started;

        public void Start()
        {
            InitializeControllers();

            InitializeRoutes();

            _httpListener = new HttpListener();

            _httpListener.Prefixes.Add(Constants.HostUrl);

            Console.WriteLine("Listening..");

            _started = true;

            _mainThread = new Thread(StartProcessingRequests);

            _httpListener.Start();

            _mainThread.Start();
        }

        private void StartProcessingRequests()
        {
            while (_started)
                ThreadPool.QueueUserWorkItem(ProcessRequest, _httpListener.GetContext());
        }

        public void Stop()
        {
            _started = false;
            _mainThread.Abort();
        }

        private void ProcessRequest(object o)
        {
            var context = (HttpListenerContext) o;
            Console.WriteLine("Rq: " + context.Request.Url);

            if (TryToHandleRequestByController(context))
                return;

            WriteDefaultError(context);
        }

        private void InitializeRoutes()
        {
            _routes.AddNewRoute().AddController().AddDefaultAction("GetAll", "all");
            _routes.AddNewRoute().AddController().AddDefaultAction("Add", "add");
            _routes.AddNewRoute().AddController().AddDefaultAction("Get", "get").AddLiteral("id").AddParameter("id");
            _routes.AddNewRoute().AddController().AddAction().AddLiteral("id").AddParameter("id");
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
                    var controllerInstance = Activator.CreateInstance(controller,
                        context);

                    var targetMethod = routeControllerMatchResult.Method;

                    var targetParameters = targetMethod.GetParameters();

                    var customAttributes = targetMethod.GetCustomAttributes(typeof(RequiredHttpMethodAttribute), true);

                    if (customAttributes.Length > 0)
                    {
                        var requiredHttpMethodAttributes = customAttributes.Cast<RequiredHttpMethodAttribute>();

                        if (
                            requiredHttpMethodAttributes.All(
                                attribute =>
                                    string.Compare(attribute.HttpMethod, context.Request.HttpMethod,
                                        StringComparison.Ordinal) != 0))
                        {
                            context.Response.StatusCode = 405;
                            context.Response.Close();

                            return true;
                        }
                    }

                    var arguments = new List<object>(targetParameters.Length);

                    for (var i = 0; i < targetParameters.Length; i++)
                    {
                        arguments.Add(null);
                    }

                    for (var i = 0; i < targetParameters.Length; i++)
                    {
                        var parameterValue = routeControllerMatchResult.MethodParameters.First(
                            param =>
                                string.Equals(param.Name, targetParameters[i].Name,
                                    StringComparison.InvariantCultureIgnoreCase));

                        arguments[i] = parameterValue.Value;
                    }

                    var result = targetMethod.Invoke(controllerInstance, arguments.ToArray());

                    if (result != null && result is ActionResult)
                    {
                        var actionResult = (ActionResult) result;

                        if (!string.IsNullOrEmpty(actionResult.Data))
                        {
                            var buffer = Encoding.UTF8.GetBytes(actionResult.Data);
                            context.Response.ContentLength64 = buffer.LongLength;
                            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                        }

                        context.Response.StatusCode = actionResult.StatusCode;
                        context.Response.ContentType = actionResult.ContentType;
                        context.Response.ContentEncoding = Encoding.UTF8;
                    }
                    else
                    {
                        context.Response.StatusCode = 200;
                    }

                    context.Response.Close();

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
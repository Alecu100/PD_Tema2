using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using CinemaRest.Controllers;
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

                    var arguments = new List<object>(targetParameters.Length);

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
                        var streamWriter = new StreamWriter(context.Response.OutputStream);
                        streamWriter.Write(actionResult.Data);
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
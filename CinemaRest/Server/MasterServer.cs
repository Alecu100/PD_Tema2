using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using CinemaRest.Controllers;
using CinemaRest.Helpers;
using CinemaRest.Routing;

namespace CinemaRest.Server
{
    internal class MasterServer
    {
        private readonly BookingController bookingController = new BookingController();
        private readonly Dictionary<string, BaseController> controllers = new Dictionary<string, BaseController>();
        private readonly MovieController movieController = new MovieController();
        private readonly RoomController roomController = new RoomController();
        private RouteCollection routes = new RouteCollection();
        private readonly ScheduleController scheduleController = new ScheduleController();
        private readonly UserController userController = new UserController();

        public void Start()
        {
            InitializeControllers();

            var web = new HttpListener();

            web.Prefixes.Add(Constants.HostUrl);

            Console.WriteLine("Listening..");

            web.Start();
            while (true)
            {
                var context = web.GetContext();
                Console.WriteLine("Rq: " + context.Request.Url);

                var controller = string.Empty;
                var action = string.Empty;
                NameValueCollection parameters;
                UrlResolver.ResolveUrl(context.Request.Url, out controller, out action, out parameters);

                var response = context.Response;
                var responseString = "Error happened";

                if (controller != string.Empty && controllers.ContainsKey(controller))
                    responseString = controllers[controller].Parse(action, parameters);

                var buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;

                var output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);

                Console.WriteLine(output);
                output.Close();
            }
            web.Stop();
        }

        private void InitializeControllers()
        {
            controllers.Add(Constants.Controllers.User, userController);
            controllers.Add(Constants.Controllers.Movie, movieController);
            controllers.Add(Constants.Controllers.Room, roomController);
            controllers.Add(Constants.Controllers.Schedule, scheduleController);
            controllers.Add(Constants.Controllers.Booking, bookingController);
        }
    }
}
using CinemaRest.Controllers;
using CinemaRest.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;

namespace CinemaRest.Server 
 {
    class MasterServer 
    {
        private UserController userController = new UserController ();
        private MovieController movieController = new MovieController ();
        private RoomController roomController = new RoomController ();
        private ScheduleController scheduleController = new ScheduleController ();
        private BookingController bookingController = new BookingController ();
        private Dictionary <string, BaseController> controllers = new Dictionary <string, BaseController> ();

        public void Start () 
        {
            InitializeControllers ();

            var web = new HttpListener ();

            web.Prefixes.Add (Constants.HostUrl);

            Console.WriteLine ("Listening..");

            web.Start ();
            while (true) {
                var context = web.GetContext ();
                Console.WriteLine ("Rq: " + context.Request.Url);

                string controller = string.Empty;
                string action = string.Empty;
                NameValueCollection parameters;
                UrlResolver.ResolveUrl (context.Request.Url, out controller, out action, out parameters);

                var response = context.Response;
                string responseString = "Error happened";

                if (controller != string.Empty && controllers.ContainsKey (controller)) {
                    responseString = controllers [controller].Parse (action, parameters);
                }
                
                var buffer = System.Text.Encoding.UTF8.GetBytes (responseString);
                response.ContentLength64 = buffer.Length;

                var output = response.OutputStream;
                output.Write (buffer, 0, buffer.Length);

                Console.WriteLine (output);
                output.Close ();

            }
            web.Stop ();
        }

        private void InitializeControllers () 
        {
            controllers.Add (Constants.Controllers.User, userController);
            controllers.Add (Constants.Controllers.Movie, movieController);
            controllers.Add (Constants.Controllers.Room, roomController);
            controllers.Add (Constants.Controllers.Schedule, scheduleController);
            controllers.Add (Constants.Controllers.Booking, bookingController);
        }
    }
}
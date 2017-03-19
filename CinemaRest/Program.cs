using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CinemaRest {
    class Program {
        static void Main (string [] args) {
            var web = new HttpListener ();

            web.Prefixes.Add ("http://localhost:8080/");
            web.Prefixes.Add ("http://localhost:8080/abc/");

            Console.WriteLine ("Listening..");

            web.Start ();
            while (true) {
                Console.WriteLine (web.GetContext ());

                var context = web.GetContext ();
                Console.WriteLine (context.Request.Url);

                var response = context.Response;

                string guid = Guid.NewGuid ().ToString ();
                string responseString = "<html><body>Hello world " + guid + "</body></html>";

                var buffer = System.Text.Encoding.UTF8.GetBytes (responseString);

                response.ContentLength64 = buffer.Length;

                var output = response.OutputStream;

                output.Write (buffer, 0, buffer.Length);

                Console.WriteLine (output);

                output.Close ();

                
            }
            web.Stop ();

            Console.ReadKey ();
        }
    }
}

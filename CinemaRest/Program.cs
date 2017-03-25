using System;
using CinemaRest.Helpers;
using CinemaRest.Server;

namespace CinemaRest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var server = new MasterServer();

            server.Start();

            Console.WriteLine("Listening on adress:  " + Constants.HostUrl);

            Console.WriteLine("Press any key to stop server");

            Console.ReadKey();

            server.Stop();
        }
    }
}
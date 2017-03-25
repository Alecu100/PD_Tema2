using System;
using CinemaRest.Helpers;
using CinemaRest.Server;

namespace CinemaRest 
{
    class Program 
    {
        static void Main (string [] args) 
        {
            MasterServer server = new MasterServer ();

            server.Start ();

            Console.WriteLine("Listening on adress:  " + Constants.HostUrl);

            Console.WriteLine("Press any key to stop server");

            Console.ReadKey();
        }
    }
}
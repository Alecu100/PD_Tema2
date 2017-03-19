using CinemaRest.Server;

namespace CinemaRest 
{
    class Program 
    {
        static void Main (string [] args) 
        {
            MasterServer server = new MasterServer ();
            server.Start ();

        }
    }
}
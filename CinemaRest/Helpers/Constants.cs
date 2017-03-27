namespace CinemaRest.Helpers
{
    public class Constants
    {
        public const string ErrorContentType = "text/plain";
        public const string HostUrl = "http://localhost:10000/";
        public const string DateFormat = "dd-MM-YY hh:mm";

        public const string JsonContentType = "application/json";

        public const int StatusCodeOk = 200;

        public const int StatusCodeError = 400;

        public const string JsonFileExtension = ".json";

        public class Controllers
        {
            public const string User = "users";
            public const string Room = "rooms";
            public const string Movie = "movies";
            public const string Schedule = "schedules";
            public const string Booking = "bookings";
        }

        public class Persistence
        {
            public const string Users = "Data/Users/";
            public const string Rooms = "Data/Rooms/";
            public const string Movies = "Data/Movies/";
            public const string Schedules = "Data/Schedules/";
            public const string Bookings = "Data/Bookings/";

            public const string User = "User";
            public const string Room = "Room";
            public const string Movie = "Movie";
            public const string Schedule = "Schedule";
            public const string Booking = "Booking";
        }
    }
}
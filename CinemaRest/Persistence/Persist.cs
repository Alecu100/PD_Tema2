using CinemaRest.Helpers;
using System.IO;

namespace CinemaRest.Persistence 
{
    class Persist 
    {
        public static UserPersistence Users;
        public static MoviePersistence Movies;
        public static RoomPersistence Rooms;
        public static SchedulePersistence Schedules;
        public static BookingPersistence Booking;

        public static void Initialize () 
        {
            Directory.CreateDirectory ("Data");
            Directory.CreateDirectory (Constants.Persistence.Movies);
            Directory.CreateDirectory (Constants.Persistence.Rooms);
            Directory.CreateDirectory (Constants.Persistence.Schedules);
            Directory.CreateDirectory (Constants.Persistence.Users);
            Directory.CreateDirectory (Constants.Persistence.Bookings);

            Users = new UserPersistence ();
            Movies = new MoviePersistence ();
            Rooms = new RoomPersistence ();
            Schedules = new SchedulePersistence ();
            Booking = new BookingPersistence ();
        }
    }
}
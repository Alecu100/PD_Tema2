using CinemaRest.Helpers;
using CinemaRest.Models;
using System.Collections.Generic;

namespace CinemaRest.Persistence {
    class BookingPersistence 
    {
        public static List<BookingModel> GetRooms () {
            List<BookingModel> result = new List<BookingModel> ();

            return result;
        }

        public static PersistenceCodes AddBooking (BookingModel booking) {
            return PersistenceCodes.Ok;
        }

        public static PersistenceCodes EditBooking (string bookingID, BookingModel booking) {
            return PersistenceCodes.Ok;
        }

        public static PersistenceCodes RemoveBooking (string bookingID) {
            return PersistenceCodes.Ok;
        }
    }
}
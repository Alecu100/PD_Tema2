using CinemaRest.Helpers;
using CinemaRest.Models;

namespace CinemaRest.Persistence
{
    internal class BookingPersistence : BasePersistence<BookingModel>
    {
        public override string GetPath()
        {
            return Constants.Persistence.Bookings;
        }

        public override string GetFilePrefix()
        {
            return Constants.Persistence.Booking;
        }
    }
}
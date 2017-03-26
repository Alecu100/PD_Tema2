using CinemaRest.Helpers;
using CinemaRest.Models;
using System.Collections.Generic;

namespace CinemaRest.Persistence {
    class BookingPersistence : BasePersistence <BookingModel>
    {
        public override string GetPath () {
            return (Constants.Persistence.Bookings);
        }
    }
}
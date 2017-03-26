using CinemaRest.Helpers;
using CinemaRest.Models;

namespace CinemaRest.Persistence {
    class RoomPersistence : BasePersistence<RoomModel> 
    {
        public override string GetPath () {
            return (Constants.Persistence.Rooms);
        }
    }
}
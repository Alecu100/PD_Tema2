using CinemaRest.Helpers;
using CinemaRest.Models;

namespace CinemaRest.Persistence
{
    internal class RoomPersistence : BasePersistence<RoomModel>
    {
        public override string GetPath()
        {
            return Constants.Persistence.Rooms;
        }

        public override string GetFilePrefix()
        {
            return Constants.Persistence.Room;
        }
    }
}
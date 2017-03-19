using CinemaRest.Helpers;
using CinemaRest.Models;
using System.Collections.Generic;

namespace CinemaRest.Persistence {
    class RoomPersistence
    {
        public static List <RoomModel> GetRooms () {
            List<RoomModel> result = new List<RoomModel> ();

            return result;
        }

        public static PersistenceCodes AddRoom (RoomModel roomID) {
            return PersistenceCodes.Ok;
        }

        public static PersistenceCodes EditRoom (string roomID, RoomModel room) {
            return PersistenceCodes.Ok;
        }

        public static PersistenceCodes RemoveRoom (string roomID) {
            return PersistenceCodes.Ok;
        }
    }
}

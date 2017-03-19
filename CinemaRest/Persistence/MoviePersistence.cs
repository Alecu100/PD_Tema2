using CinemaRest.Helpers;
using CinemaRest.Models;
using System.Collections.Generic;

namespace CinemaRest.Persistence {
    class MoviePersistence
    {
        public static List<MovieModel> GetRooms () {
            List<MovieModel> result = new List<MovieModel> ();

            return result;
        }

        public static PersistenceCodes AddMovie (MovieModel movieID) {
            return PersistenceCodes.Ok;
        }

        public static PersistenceCodes EditMovie (string movieID, MovieModel movie) {
            return PersistenceCodes.Ok;
        }

        public static PersistenceCodes RemoveMovie (string movieID) {
            return PersistenceCodes.Ok;
        }
    }
}

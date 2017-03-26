using CinemaRest.Helpers;
using CinemaRest.Models;

namespace CinemaRest.Persistence {
    class MoviePersistence : BasePersistence<MovieModel>
    {
        public override string GetPath () {
            return (Constants.Persistence.Movies);
        }
    }
}
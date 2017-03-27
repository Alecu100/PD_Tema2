using CinemaRest.Helpers;
using CinemaRest.Models;

namespace CinemaRest.Persistence
{
    internal class MoviePersistence : BasePersistence<MovieModel>
    {
        public override string GetPath()
        {
            return Constants.Persistence.Movies;
        }

        public override string GetFilePrefix()
        {
            return Constants.Persistence.Movie;
        }
    }
}
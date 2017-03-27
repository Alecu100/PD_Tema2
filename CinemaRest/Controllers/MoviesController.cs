using System;
using System.IO;
using System.Linq;
using System.Net;
using CinemaRest.Controllers.ActionResults;
using CinemaRest.Controllers.Authorization;
using CinemaRest.Helpers;
using CinemaRest.Models;
using CinemaRest.Persistence;
using Newtonsoft.Json;

namespace CinemaRest.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MoviePersistence _moviePersistence;

        public MoviesController(HttpListenerContext context) : base(context)
        {
            _moviePersistence = new MoviePersistence();
        }

        [RequiredHttpGet]
        public ActionResult GetAll()
        {
            var allMovies = _moviePersistence.GetAll();

            return Json(allMovies);
        }

        [RequiredHttpGet]
        public ActionResult Get(string id)
        {
            var foundMovie = _moviePersistence.GetAll().FirstOrDefault(movie => string.Compare(movie.ID, id,
                StringComparison.InvariantCultureIgnoreCase) == 0);

            if (foundMovie == null)
            {
                return Error();
            }

            return Json(foundMovie);
        }

        [RequiredHttpPut]
        public ActionResult Add()
        {
            var streamReader = new StreamReader(_context.Request.InputStream);
            var jsonSerializer = new JsonSerializer();
            var movieModel = jsonSerializer.Deserialize<MovieModel>(new JsonTextReader(streamReader));

            if (_moviePersistence.Add(movieModel) == PersistenceCodes.IdAlreadyUsed)
            {
                return Error();
            }

            return Json(movieModel);
        }

        [RequiredHttpPost]
        public ActionResult Edit(string id)
        {
            var streamReader = new StreamReader(_context.Request.InputStream);
            var jsonSerializer = new JsonSerializer();
            var movieModel = jsonSerializer.Deserialize<MovieModel>(new JsonTextReader(streamReader));

            if (_moviePersistence.Edit(id, movieModel) == PersistenceCodes.IdNotFound)
            {
                return Error();
            }

            return Ok();
        }


        [RequiredHttpDelete]
        public ActionResult Delete(string id)
        {
            if (_moviePersistence.Remove(id) == PersistenceCodes.IdNotFound)
            {
                return Error();
            }

            return Ok();
        }
    }
}
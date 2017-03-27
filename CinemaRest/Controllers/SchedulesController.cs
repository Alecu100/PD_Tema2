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
    public class SchedulesController : Controller
    {
        private readonly MoviePersistence _moviePersistence;
        private readonly RoomPersistence _roomPersistence;
        private readonly SchedulePersistence _schedulePersistence;

        public SchedulesController(HttpListenerContext context) : base(context)
        {
            _schedulePersistence = new SchedulePersistence();
            _roomPersistence = new RoomPersistence();
            _moviePersistence = new MoviePersistence();
        }

        [RequiredHttpGet]
        public ActionResult GetAll()
        {
            var allMovies = _schedulePersistence.GetAll();

            return Json(allMovies);
        }

        [RequiredHttpGet]
        public ActionResult Get(string id)
        {
            var foundMovie = _schedulePersistence.GetAll().FirstOrDefault(movie => string.Compare(movie.ID, id,
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
            var scheduleModel = jsonSerializer.Deserialize<ScheduleModel>(new JsonTextReader(streamReader));

            if (
                _moviePersistence.GetAll()
                    .All(
                        movie =>
                            string.Compare(movie.ID, scheduleModel.MovieID, StringComparison.InvariantCultureIgnoreCase) !=
                            0))
            {
                return Error();
            }

            if (
                _roomPersistence.GetAll()
                    .All(
                        room =>
                            string.Compare(room.ID, scheduleModel.RoomID, StringComparison.InvariantCultureIgnoreCase) !=
                            0))
            {
                return Error();
            }

            if (_schedulePersistence.Add(scheduleModel) == PersistenceCodes.IdAlreadyUsed)
            {
                return Error();
            }

            return Json(scheduleModel);
        }

        [RequiredHttpPost]
        public ActionResult Edit(string id)
        {
            var streamReader = new StreamReader(_context.Request.InputStream);
            var jsonSerializer = new JsonSerializer();
            var scheduleModel = jsonSerializer.Deserialize<ScheduleModel>(new JsonTextReader(streamReader));

            if (
                _moviePersistence.GetAll()
                    .All(
                        movie =>
                            string.Compare(movie.ID, scheduleModel.MovieID, StringComparison.InvariantCultureIgnoreCase) !=
                            0))
            {
                return Error();
            }

            if (
                _roomPersistence.GetAll()
                    .All(
                        room =>
                            string.Compare(room.ID, scheduleModel.RoomID, StringComparison.InvariantCultureIgnoreCase) !=
                            0))
            {
                return Error();
            }

            if (_schedulePersistence.Edit(id, scheduleModel) == PersistenceCodes.IdNotFound)
            {
                return Error();
            }

            return Ok();
        }


        [RequiredHttpDelete]
        public ActionResult Delete(string id)
        {
            if (_schedulePersistence.Remove(id) == PersistenceCodes.IdNotFound)
            {
                return Error();
            }

            return Ok();
        }
    }
}
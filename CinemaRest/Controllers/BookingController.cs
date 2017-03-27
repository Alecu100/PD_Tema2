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
    public class BookingController : Controller
    {
        private readonly BookingPersistence _bookingPersistence;
        private readonly SchedulePersistence _schedulePersistence;
        private readonly UserPersistence _userPersistence;

        public BookingController(HttpListenerContext context) : base(context)
        {
            _schedulePersistence = new SchedulePersistence();
            _userPersistence = new UserPersistence();
            _bookingPersistence = new BookingPersistence();
        }

        [RequiredHttpGet]
        public ActionResult GetAll()
        {
            var allMovies = _bookingPersistence.GetAll();

            return Json(allMovies);
        }

        [RequiredHttpGet]
        public ActionResult Get(string id)
        {
            var foundMovie = _bookingPersistence.GetAll().FirstOrDefault(movie => string.Compare(movie.ID, id,
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
            var bookingModel = jsonSerializer.Deserialize<BookingModel>(new JsonTextReader(streamReader));

            if (
                _schedulePersistence.GetAll()
                    .All(
                        schedule =>
                            string.Compare(schedule.ID, bookingModel.ScheduleID,
                                StringComparison.InvariantCultureIgnoreCase) !=
                            0))
            {
                return Error();
            }

            if (
                _userPersistence.GetAll()
                    .All(
                        user =>
                            string.Compare(user.ID, bookingModel.UserID, StringComparison.InvariantCultureIgnoreCase) !=
                            0))
            {
                return Error();
            }

            if (_bookingPersistence.Add(bookingModel) == PersistenceCodes.IdAlreadyUsed)
            {
                return Error();
            }

            return Json(bookingModel);
        }

        [RequiredHttpPost]
        public ActionResult Edit(string id)
        {
            var streamReader = new StreamReader(_context.Request.InputStream);
            var jsonSerializer = new JsonSerializer();
            var bookingModel = jsonSerializer.Deserialize<BookingModel>(new JsonTextReader(streamReader));

            if (
                _schedulePersistence.GetAll()
                    .All(
                        schedule =>
                            string.Compare(schedule.ID, bookingModel.ScheduleID,
                                StringComparison.InvariantCultureIgnoreCase) !=
                            0))
            {
                return Error();
            }

            if (
                _userPersistence.GetAll()
                    .All(
                        user =>
                            string.Compare(user.ID, bookingModel.UserID, StringComparison.InvariantCultureIgnoreCase) !=
                            0))
            {
                return Error();
            }

            if (_bookingPersistence.Edit(id, bookingModel) == PersistenceCodes.IdNotFound)
            {
                return Error();
            }

            return Ok();
        }


        [RequiredHttpDelete]
        public ActionResult Delete(string id)
        {
            if (_bookingPersistence.Remove(id) == PersistenceCodes.IdNotFound)
            {
                return Error();
            }

            return Ok();
        }
    }
}
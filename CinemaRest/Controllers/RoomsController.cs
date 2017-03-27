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
    public class RoomsController : Controller
    {
        private readonly RoomPersistence _roomPersistence;

        public RoomsController(HttpListenerContext context) : base(context)
        {
            _roomPersistence = new RoomPersistence();
        }

        [RequiredHttpGet]
        public ActionResult GetAll()
        {
            var allMovies = _roomPersistence.GetAll();

            return Json(allMovies);
        }

        [RequiredHttpGet]
        public ActionResult Get(string id)
        {
            var foundRoom = _roomPersistence.GetAll().FirstOrDefault(room => string.Compare(room.ID, id,
                StringComparison.InvariantCultureIgnoreCase) == 0);

            if (foundRoom == null)
            {
                return Error();
            }

            return Json(foundRoom);
        }

        [RequiredHttpPut]
        public ActionResult Add()
        {
            var streamReader = new StreamReader(_context.Request.InputStream);
            var jsonSerializer = new JsonSerializer();
            var roomModel = jsonSerializer.Deserialize<RoomModel>(new JsonTextReader(streamReader));

            if (_roomPersistence.Add(roomModel) == PersistenceCodes.IdAlreadyUsed)
            {
                return Error();
            }

            return Json(roomModel);
        }

        [RequiredHttpPost]
        public ActionResult Edit(string id)
        {
            var streamReader = new StreamReader(_context.Request.InputStream);
            var jsonSerializer = new JsonSerializer();
            var roomModel = jsonSerializer.Deserialize<RoomModel>(new JsonTextReader(streamReader));

            if (_roomPersistence.Edit(id, roomModel) == PersistenceCodes.IdNotFound)
            {
                return Error();
            }

            return Ok();
        }


        [RequiredHttpDelete]
        public ActionResult Delete(string id)
        {
            if (_roomPersistence.Remove(id) == PersistenceCodes.IdNotFound)
            {
                return Error();
            }

            return Ok();
        }
    }
}
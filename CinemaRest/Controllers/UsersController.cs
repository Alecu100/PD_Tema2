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
    public class UsersController : Controller
    {
        private readonly UserPersistence _userPersistence;

        public UsersController(HttpListenerContext context) : base(context)
        {
            _userPersistence = new UserPersistence();
        }

        [RequiredHttpGet]
        public ActionResult GetAll()
        {
            var allUsers = _userPersistence.GetAll();

            return Json(allUsers);
        }

        [RequiredHttpGet]
        public ActionResult Get(string id)
        {
            var foundMovie = _userPersistence.GetAll().FirstOrDefault(user => string.Compare(user.ID, id,
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
            var userModel = jsonSerializer.Deserialize<UserModel>(new JsonTextReader(streamReader));

            if (_userPersistence.Add(userModel) == PersistenceCodes.IdAlreadyUsed)
            {
                return Error();
            }

            return Json(userModel);
        }

        [RequiredHttpPost]
        public ActionResult Edit(string id)
        {
            var streamReader = new StreamReader(_context.Request.InputStream);
            var jsonSerializer = new JsonSerializer();
            var userModel = jsonSerializer.Deserialize<UserModel>(new JsonTextReader(streamReader));

            if (_userPersistence.Edit(id, userModel) == PersistenceCodes.IdNotFound)
            {
                return Error();
            }

            return Ok();
        }


        [RequiredHttpDelete]
        public ActionResult Delete(string id)
        {
            if (_userPersistence.Remove(id) == PersistenceCodes.IdNotFound)
            {
                return Error();
            }

            return Ok();
        }
    }
}
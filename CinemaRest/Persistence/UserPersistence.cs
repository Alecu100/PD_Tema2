using CinemaRest.Helpers;
using CinemaRest.Models;
using System.Collections.Generic;
using System.IO;

namespace CinemaRest.Persistence {
    class UserPersistence 
    {
        public static List <UserModel> GetUsers () {
            List <UserModel> result = new List<UserModel> ();

            return result;
        }

        public static PersistenceCodes AddUser (UserModel newUser) {
            string newUUID = System.Guid.NewGuid ().ToString ();
            string userFile = Constants.Persistence.Users + newUUID;

            if (File.Exists (userFile)) {
                return PersistenceCodes.IdAlreadyUsed;
            }

            

            return PersistenceCodes.Ok;
        }

        public static PersistenceCodes EditUser (string userID, UserModel user) {
            return PersistenceCodes.Ok;
        }

        public static PersistenceCodes RemoveUser (string userID) {
            return PersistenceCodes.Ok;
        }
    }
}
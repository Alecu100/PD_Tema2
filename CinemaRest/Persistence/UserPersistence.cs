using CinemaRest.Helpers;
using CinemaRest.Models;

namespace CinemaRest.Persistence {
    class UserPersistence : BasePersistence <UserModel>
    {
        public override string GetPath () 
        {
            return (Constants.Persistence.Users);
        }
    }
}
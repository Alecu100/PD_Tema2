using CinemaRest.Helpers;
using CinemaRest.Models;

namespace CinemaRest.Persistence
{
    internal class UserPersistence : BasePersistence<UserModel>
    {
        public override string GetPath()
        {
            return Constants.Persistence.Users;
        }

        public override string GetFilePrefix()
        {
            return Constants.Persistence.User;
        }
    }
}
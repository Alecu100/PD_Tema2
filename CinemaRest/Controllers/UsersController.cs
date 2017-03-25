using System.Net;

namespace CinemaRest.Controllers
{
    public class UsersController : Controller
    {
        public UsersController(HttpListenerContext context) : base(context)
        {
        }
    }
}
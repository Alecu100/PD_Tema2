using System.Net;

namespace CinemaRest.Controllers
{
    public class UserController : Controller
    {
        public UserController(HttpListenerContext context) : base(context)
        {
        }
    }
}
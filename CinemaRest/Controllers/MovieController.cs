using System.Net;

namespace CinemaRest.Controllers
{
    public class MovieController : Controller
    {
        public MovieController(HttpListenerContext context) : base(context)
        {
        }
    }
}
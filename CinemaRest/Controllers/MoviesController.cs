using System.Net;

namespace CinemaRest.Controllers
{
    public class MoviesController : Controller
    {
        public MoviesController(HttpListenerContext context) : base(context)
        {
        }
    }
}
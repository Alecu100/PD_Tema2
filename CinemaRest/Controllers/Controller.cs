using System.Net;

namespace CinemaRest.Controllers
{
    public abstract class Controller
    {
        protected HttpListenerContext _context;

        protected Controller(HttpListenerContext context)
        {
            _context = context;
        }
    }
}
using System.Net;

namespace CinemaRest.Controllers
{
    public class BookingController : Controller
    {
        public BookingController(HttpListenerContext context) : base(context)
        {
        }

        public void GetAll()
        {
        }
    }
}
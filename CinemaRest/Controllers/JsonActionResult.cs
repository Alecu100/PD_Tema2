using CinemaRest.Helpers;

namespace CinemaRest.Controllers
{
    public class JsonActionResult : ActionResult
    {
        public override string ContentType
        {
            get { return Constants.JsonContentType; }
        }

        public override int StatusCode
        {
            get { return 200; }
        }
    }
}
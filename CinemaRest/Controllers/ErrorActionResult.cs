using CinemaRest.Helpers;

namespace CinemaRest.Controllers
{
    public class ErrorActionResult : ActionResult
    {
        public override string ContentType
        {
            get { return Constants.ErrorContentType; }
        }

        public override int StatusCode
        {
            get { return Constants.StatusCodeError; }
        }
    }
}
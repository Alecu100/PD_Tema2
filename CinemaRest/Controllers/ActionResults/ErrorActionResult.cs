using CinemaRest.Helpers;

namespace CinemaRest.Controllers.ActionResults
{
    public class ErrorActionResult : ActionResult
    {
        public override string ContentType
        {
            get { return Constants.DefaultContentType; }
        }

        public override int StatusCode
        {
            get { return Constants.StatusCodeError; }
        }
    }
}
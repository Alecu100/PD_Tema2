using CinemaRest.Helpers;

namespace CinemaRest.Controllers.ActionResults
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
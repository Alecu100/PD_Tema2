namespace CinemaRest.Controllers.ActionResults
{
    public abstract class ActionResult
    {
        public string Data { get; set; }

        public abstract string ContentType { get; }

        public abstract int StatusCode { get; }
    }
}
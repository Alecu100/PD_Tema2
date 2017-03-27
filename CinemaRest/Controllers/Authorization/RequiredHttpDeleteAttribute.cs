namespace CinemaRest.Controllers.Authorization
{
    public class RequiredHttpDeleteAttribute : RequiredHttpMethodAttribute
    {
        public override string HttpMethod
        {
            get { return "DELETE"; }
        }
    }
}
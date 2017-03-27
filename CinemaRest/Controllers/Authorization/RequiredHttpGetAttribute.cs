using System;

namespace CinemaRest.Controllers.Authorization
{
    [Serializable]
    public class RequiredHttpGetAttribute : RequiredHttpMethodAttribute
    {
        public override string HttpMethod
        {
            get { return "GET"; }
        }
    }
}
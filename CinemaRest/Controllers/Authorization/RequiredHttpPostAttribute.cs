using System;

namespace CinemaRest.Controllers.Authorization
{
    [Serializable]
    public class RequiredHttpPostAttribute : RequiredHttpMethodAttribute
    {
        public override string HttpMethod
        {
            get { return "POST"; }
        }
    }
}
using System;

namespace CinemaRest.Controllers.Authorization
{
    [Serializable]
    public class RequiredHttpPutAttribute : RequiredHttpMethodAttribute
    {
        public override string HttpMethod
        {
            get { return "PUT"; }
        }
    }
}
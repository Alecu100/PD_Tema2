using System;

namespace CinemaRest.Controllers.Authorization
{
    [Serializable]
    public abstract class RequiredHttpMethodAttribute : Attribute
    {
        public abstract string HttpMethod { get; }
    }
}
using System.Collections.Specialized;

namespace CinemaRest.Controllers
{
    public abstract class BaseController
    {
        public abstract string Parse(string method, NameValueCollection paramters);
    }
}
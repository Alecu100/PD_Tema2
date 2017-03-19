using System.Collections.Specialized;

namespace CinemaRest.Controllers {
    abstract class BaseController 
    {
        public abstract string Parse (string method, NameValueCollection paramters);
    }
}
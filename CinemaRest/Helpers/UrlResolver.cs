using System;
using System.Collections.Specialized;
using System.Web;

namespace CinemaRest.Helpers {
    class UrlResolver {

        public static void ResolveUrl (Uri url, out string controller, out string action, out NameValueCollection parameters) {
            controller = string.Empty;
            if (url.Segments.Length > 1) 
            {
                controller = url.Segments [1];
            }

            action = string.Empty;
            if (url.Segments.Length > 2) 
            {
                action = url.Segments [2];
            }

            parameters = HttpUtility.ParseQueryString (url.ToString ());
        }

    }
}
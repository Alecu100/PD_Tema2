using System.IO;
using System.Net;
using CinemaRest.Controllers.ActionResults;
using Newtonsoft.Json;

namespace CinemaRest.Controllers
{
    public abstract class Controller
    {
        protected HttpListenerContext _context;

        protected Controller(HttpListenerContext context)
        {
            _context = context;
        }

        protected JsonActionResult Json(object data)
        {
            var jsonActionResult = new JsonActionResult();
            var serializer = new JsonSerializer();
            var stringWriter = new StringWriter();
            var jsonTextWriter = new JsonTextWriter(stringWriter);

            serializer.Serialize(jsonTextWriter, data);
            jsonActionResult.Data = stringWriter.GetStringBuilder().ToString();

            return jsonActionResult;
        }

        protected ErrorActionResult Error()
        {
            return new ErrorActionResult();
        }

        protected OkActionResult Ok()
        {
            return new OkActionResult();
        }
    }
}
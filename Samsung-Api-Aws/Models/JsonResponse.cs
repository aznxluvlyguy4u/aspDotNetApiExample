using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Net;

namespace samsung.api.Models
{
    public class JsonResponse : JObject
    {
        private const string ServerErrorMessage = "An internal server error has occurred";
        private const string UnauthorizedMessage = "Unauthorized";
        private const string ForbiddenMessage = "Forbidden";
        private const string NotFoundMessage = "Not Found";

        private static readonly JsonSerializer NullHandlingSerializer = new JsonSerializer
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public JsonResponse(object payload, HttpStatusCode httpStatusCode)
        {
            Add("code", (int)httpStatusCode);
            Add("status", httpStatusCode.ToString());

            // Determine if it is error range code
            if ((int)httpStatusCode >= (int)HttpStatusCode.BadRequest)
            {
                Add("error", GetErrorMessage(payload, httpStatusCode));
                return;
            }

            try
            {
                if (payload == null)
                {
                    Add("data", "");
                    return;
                }

                if (
                    ((payload is ICollection || payload is IEnumerable) && !(payload is IDictionary))
                    || payload.GetType().IsArray)
                {
                    Add("data", JArray.FromObject(payload, NullHandlingSerializer));
                    return;
                }

                Add("data", JObject.FromObject(payload, NullHandlingSerializer));
            }
            catch (Exception ex)
            {
                Property("code").Remove();
                Property("status").Remove();

                Add("code", 500);
                Add("status", nameof(HttpStatusCode.InternalServerError));

#if DEBUG
                Add("error", ex.Message);
#else
                Add("error", ServerErrorMessage);
#endif
            }
        }

        private string GetErrorMessage(object payload, HttpStatusCode httpStatusCode)
        {
            if (payload != null && (payload is string))
                return payload.ToString();

            switch (httpStatusCode)
            {
                case HttpStatusCode.NotFound: return NotFoundMessage;
                case HttpStatusCode.Forbidden: return ForbiddenMessage;
                case HttpStatusCode.Unauthorized: return UnauthorizedMessage;
                case HttpStatusCode.InternalServerError: return ServerErrorMessage;
                default: return httpStatusCode.ToString();
            }
        }
    }
}
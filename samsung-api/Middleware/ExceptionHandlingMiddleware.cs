using Microsoft.AspNetCore.Http;
using samsung.api.Models;
using samsung_api.Extensions;
using samsung_api.Services.Logger;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace samsung.api.Middleware
{
    /// <summary>
    /// <para>
    /// This middleware will handle all uncaught exceptions.
    /// Allowing us to return a well formated message towards the invoking party.
    /// This also allows us to log exceptions to other sources as we see fit.
    /// </para>
    /// <para>
    /// Exception handling middleware as based upon
    /// asp-net-core-web-api-exception-handling:
    /// https://stackoverflow.com/a/38935583/2030635
    /// </para>
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context, ILogger logger)
        {
            _logger = logger;
            try
            {
                // Switcheroo to allow stream manipulation when response has started
                var originBody = context.Response.Body;
                var newBody = new MemoryStream();
                context.Response.Body = newBody;

                await _next(context).ConfigureAwait(false);

                // Get original response body
                newBody.Seek(0, SeekOrigin.Begin);
                string json = new StreamReader(newBody).ReadToEnd();
                json.TryDeserializeJson(out dynamic jResult);
                context.Response.Body = originBody;

                // Catch kestrel generated exception messages
                if (context.Response.StatusCode == 400 && jResult?.code == null)
                {
                    json = ((object)new JsonResponse(json, HttpStatusCode.BadRequest)).ToJson();
                }

                context.Response.StatusCode = jResult?.code ?? HttpStatusCode.BadRequest;

                // Write (manipulated) response back to original stream.
                await context.Response.WriteAsync(json).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex).ConfigureAwait(false);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            await _logger.LogErrorAsync("Uncaught exception", exception).ConfigureAwait(false);
#if DEBUG
            var result = new JsonResponse(exception.Message, HttpStatusCode.InternalServerError);
#else
            var result = new JsonResponse(null, HttpStatusCode.InternalServerError);
#endif
            var json = ((object)result).ToJson();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            await context.Response.WriteAsync(json).ConfigureAwait(false);
        }
    }
}
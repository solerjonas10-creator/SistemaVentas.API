using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SistemaVentas.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate? _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                if (_next != null)
                {
                    await _next(httpContext);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json"; 
            context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetails()
            {
                StatusCode = StatusCodes.Status406NotAcceptable,
                Message = "Algo salio mal. Error!",
                StackTrace = exception.StackTrace
            }));
        }
    }
}

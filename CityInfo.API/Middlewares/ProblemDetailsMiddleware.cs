using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CityInfo.API.Middlewares
{
    public class ProblemDetailsMiddleware
    {

        #region Private Fields

        private readonly RequestDelegate _next;

        #endregion

        #region Constructor

        public ProblemDetailsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region Public Methods

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        #endregion

        #region Private Methods

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred",
                Detail = exception.Message,
                Instance = context.Request.Path,
                Extensions = 
                {
                    { "traceId", context.TraceIdentifier },
                    { "server", Environment.MachineName },
                    { "timestamp", DateTime.UtcNow.ToString("o") }
                }
            };

            var json = JsonSerializer.Serialize(problemDetails);
            return context.Response.WriteAsync(json);
        }

        #endregion

    }
}

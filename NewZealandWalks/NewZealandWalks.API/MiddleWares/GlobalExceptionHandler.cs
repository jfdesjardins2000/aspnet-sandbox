using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace NewZealandWalks.API.MiddleWares
{
    /// <summary>
    /// https://www.milanjovanovic.tech/blog/global-error-handling-in-aspnetcore-8
    /// </summary>

    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;


        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                // Log This Exception
                _logger.LogError(ex, $"{errorId} : {ex.Message}");


                // On limite l'information qu'on va retourner a l'utilisateur
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "OUCH!!! WTF !!! QUESSE TA FAIT ENCORE!!!",
                    Detail = "A human-readable explanation specific to this occurrence of the proble"
                };

                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";

                // Return A Custom Exrror Response
                await httpContext.Response.WriteAsJsonAsync(problemDetails);

            }
        }

    }
}

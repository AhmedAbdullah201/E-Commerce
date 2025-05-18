using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                await HandleNotFoundPathAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleNotFoundPathAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound && !httpContext.Response.HasStarted)
            {
                httpContext.Response.ContentType = "application/json";
                var response = new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ErrorMessages = $"The endpoint {httpContext.Request.Path} was not found."
                };

                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";

            var response = new ErrorDetails
            {
                ErrorMessages = ex.Message
            };
            var statusCode = ex switch
            {
                UserNotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException => GetValidationErrors(badRequestException , response),
                _ => StatusCodes.Status500InternalServerError
            };

            httpContext.Response.StatusCode = response.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(response);
        }

        private int GetValidationErrors(BadRequestException badRequestException, ErrorDetails response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}

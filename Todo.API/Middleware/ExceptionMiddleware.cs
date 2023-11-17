using Microsoft.AspNetCore.Mvc;
using System.Net;
using TodoApp.Application.Common.Exceptions;

namespace TodoApp.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            ProblemDetails problemDetails;
            string logMessage;

            switch (exception)
            {
                case ServiceException ex:
                    response.StatusCode = ex.StatusCode;
                    problemDetails = ex.ProblemDetails;
                    logMessage = ex.DefaultMessage;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    problemDetails = new()
                    {
                        Status = response.StatusCode,
                        Type = "https://example.com/errors/internal",
                        Title = "Internal Server Error",
                        Detail = exception.Message
                    };
                    logMessage = exception.Message;
                    break;
            }

            _logger.LogError(exception, logMessage);
            await response.WriteAsJsonAsync(problemDetails);
        }
    }
}

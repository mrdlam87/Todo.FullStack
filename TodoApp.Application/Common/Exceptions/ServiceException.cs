using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Application.Common.Exceptions
{
    public class ServiceException : Exception
    {
        public ExceptionType Type { get; }
        public int StatusCode { get => GetStatusCode(); }
        public string Title { get => GetTitle(); }
        public string DefaultMessage { get => GetDefaultMessage(); }
        public ProblemDetails ProblemDetails { get; }

        public ServiceException(ExceptionType type, string? message = null, string? details = null)
            : base(message)
        {
            Type = type;
            ProblemDetails = new()
            {
                Status = StatusCode,
                Type = $"https://example.com/errors/{Type}",
                Title = Title,
                Detail = details ?? message ?? DefaultMessage
            };
        }

        public static ServiceException Invalid() => new(ExceptionType.Invalid);
        public static ServiceException Invalid(string[] errors) => new(ExceptionType.Invalid, string.Join(" ", errors));
        public static ServiceException Missing() => new(ExceptionType.Missing);
        public static ServiceException Insufficient() => new(ExceptionType.Insufficient);
        public static ServiceException Unauthorized() => new(ExceptionType.Unauthorized);
        public static ServiceException Forbidden() => new(ExceptionType.Forbidden);
        public static ServiceException NotFound() => new(ExceptionType.NotFound);
        public static ServiceException NotFound(string propertyName)
            => new(ExceptionType.NotFound, $"The requested {propertyName} resource was not found.");
        public static ServiceException Conflict() => new(ExceptionType.Conflict);
        public static ServiceException Internal() => new(ExceptionType.Internal);

        private int GetStatusCode()
        {
            return Type switch
            {
                ExceptionType.Invalid or ExceptionType.Missing or ExceptionType.Insufficient => 400,
                ExceptionType.Unauthorized => 401,
                ExceptionType.Forbidden => 403,
                ExceptionType.NotFound => 404,
                ExceptionType.Conflict => 409,
                ExceptionType.Internal => 500,
                _ => 0,
            };
        }

        private string GetTitle()
        {
            return Type switch
            {
                ExceptionType.Unauthorized => "Unauthorized",
                ExceptionType.Forbidden => "Forbidden",
                ExceptionType.NotFound => "Not Found",
                ExceptionType.Conflict => "Conflict",
                _ => "Bad Request",
            };
        }

        private string GetDefaultMessage()
        {
            return Type switch
            {
                ExceptionType.Unauthorized => "You are not authorized to perform this action.",
                ExceptionType.Forbidden => "Access to this resource is forbidden.",
                ExceptionType.NotFound => "The requested resource was not found.",
                ExceptionType.Conflict => "The request could not be completed due to a conflict with the current state of the resource.",
                ExceptionType.Invalid => "The request was invalid.",
                ExceptionType.Missing => "The request is missing required data.",
                ExceptionType.Insufficient => "The request is missing sufficient data to complete the action.",
                _ => "An internal server error has occurred.",
            };
        }
    }
}

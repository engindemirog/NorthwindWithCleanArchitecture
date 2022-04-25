using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            object errors = null;

            if (exception.GetType() == typeof(ValidationException))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errors = ((ValidationException)exception).Errors;

                return context.Response.WriteAsync(new ValidationProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://example.com/probs/validation",
                    Title = "Validation error(s)",
                    Detail = "",
                    Instance = "",
                    Errors = errors
                }.ToString());

            }

            if (exception.GetType() == typeof(BusinessException))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return context.Response.WriteAsync(new BusinessProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://example.com/probs/business",
                    Title = "Business exception",
                    Detail = exception.Message,
                    Instance = ""
                }.ToString());

            }

            return context.Response.WriteAsync(new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://example.com/probs/internal",
                Title = "Internal exception",
                Detail = exception.Message,
                Instance = ""
            }.ToString());
        }
    }
}

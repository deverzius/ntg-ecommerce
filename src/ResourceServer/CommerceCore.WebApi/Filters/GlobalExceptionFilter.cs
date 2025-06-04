using CommerceCore.Shared.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CommerceCore.WebApi.Filters;

public class GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IWebHostEnvironment environment) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        logger.LogError("An unhandled exception occurred: {Exception}", context.Exception);

        var stackTrace = environment.IsDevelopment() ? context.Exception.StackTrace : string.Empty;

        switch (context.Exception)
        {
            case AppException appException:
                context.Result = new ObjectResult(new
                {
                    Message = appException.Message,
                    stackTrace
                })
                {
                    StatusCode = appException.StatusCode,
                };
                break;

            case ValidationException validationException:
                context.Result = new ObjectResult(new
                {
                    Message = "Validation error",
                    stackTrace,
                    Errors = validationException.Errors.Select(error => new
                    {
                        PropertyName = error.PropertyName,
                        ErrorMessage = error.ErrorMessage,
                        AttemptedValue = error.AttemptedValue
                    })
                })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
                break;

            default:
                context.Result = new ObjectResult(new
                {
                    Message = context.Exception.Message,
                    stackTrace
                })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                break;
        }

        context.ExceptionHandled = true;
    }
}
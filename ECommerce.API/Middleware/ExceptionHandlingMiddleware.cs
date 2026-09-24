using ECommerce.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ECommerce.Domain.Exceptions;
using FluentValidation;

namespace ECommerce.API.Middleware;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status400BadRequest,
            DomainException => StatusCodes.Status409Conflict,
            BusinessRuleException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = statusCode switch
            {
                StatusCodes.Status404NotFound => "Resource not found.",
                StatusCodes.Status400BadRequest => "Validation failed.",
                StatusCodes.Status409Conflict => "Business rule violation",
                _ => "An unhandled exception occurred."
            },
            Detail = statusCode switch
            {
                StatusCodes.Status404NotFound => exception.Message,
                StatusCodes.Status409Conflict => exception.Message,
                _ => null
            },
            Instance = context.Request.Path
        };

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions["errors"] = validationException.Errors
                .GroupBy(error => error.PropertyName).ToDictionary(group => group.Key,
                    group => group.Select(error => error.ErrorMessage).ToArray());
        }

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}
using System.Net;
using FluentValidation;
using Newtonsoft.Json;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var errors = exception.Errors
            .Select(e => new
            {
                Field = e.PropertyName,
                Message = e.ErrorMessage
            });

        var result = JsonConvert.SerializeObject(new
        {
            Success = false,
            Errors = errors
        });

        return context.Response.WriteAsync(result);
    }
}
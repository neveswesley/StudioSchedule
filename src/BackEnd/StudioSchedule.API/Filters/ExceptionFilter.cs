using System.Net;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudioSchedule.Communications.Responses;
using StudioSchedule.Exceptions;

namespace StudioSchedule.WebAPI.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is StudioScheduleException)
            HandleProjectException(context);

        else
            ThrowUnknowException(context);
    }

    private void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is StudioScheduleException)
        {
            var exception = context.Exception as ErrorOnValidationException;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.ErrorMessages));
        }
    }

    private void ThrowUnknowException(ExceptionContext context)
    {
        if (context.ExceptionHandled is ErrorOnValidationException)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOW_ERROR));
        }
    }
    
}
using System.Net;
using Contacts.Api.Exceptions;

namespace Contacts.Api.Middlewares;

public class CustomExceptionHandlerMiddleware(
    ILogger<CustomExceptionHandlerMiddleware> logger,
    RequestDelegate next)
{
public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred");

            (int code, string message) = ex switch
            {
                CustomConflictException conflict => ((int)HttpStatusCode.Conflict, conflict.Message),
                CustomNotFoundException notFound => ((int)HttpStatusCode.NotFound, notFound.Message),
                _ => ((int)HttpStatusCode.InternalServerError, ex.Message)
            };

            context.Response.StatusCode = code;
            await context.Response.WriteAsync(message);
        }
    }
}
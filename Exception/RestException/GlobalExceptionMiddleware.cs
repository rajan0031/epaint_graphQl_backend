using System.Net;
using System.Text.Json;
using MyGraphqlApp.Exception.UserException;
using MyGraphqlApp.Exception.GlobalException;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UserException ex)
        {
            await HandleExceptionAsync(context, ex, ex.StatusCode);
        }
        catch (GlobalException ex)
        {
            await HandleExceptionAsync(context, ex, ex.StatusCode);
        }
        catch (Exception ex)
        {
            // fallback for unexpected errors
            await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            status = context.Response.StatusCode,
            error = statusCode.ToString(),
            message = exception.Message
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}

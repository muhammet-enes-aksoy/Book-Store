using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;

namespace BookStore.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = Stopwatch.StartNew();
        try
        {
            var message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
            Console.WriteLine(message);
            await _next(context);
            watch.Stop();
            message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path + "responded " 
                      + context.Response.StatusCode + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds +  "ms";
            Console.WriteLine(message);
        }
        catch (Exception e)
        {
            watch.Stop();
            await HandleException(context, e, watch);
        }
    }

    private Task HandleException(HttpContext context, Exception exception, Stopwatch watch)
    {
        var message = "[Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode
                      + "Error Message" + exception.Message + exception.Message + " in "
                      + watch.Elapsed.TotalMilliseconds + " ms";
        Console.WriteLine(message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonConvert.SerializeObject(new { error = exception.Message }, Formatting.None);
        return context.Response.WriteAsync(result);
    }
}

public static class CustomerExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
}
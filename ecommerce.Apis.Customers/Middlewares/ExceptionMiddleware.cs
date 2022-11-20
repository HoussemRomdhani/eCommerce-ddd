using eCommerce.Domain.SharedKernel.Errors;
using System.Net;
using System.Text.Json;

namespace ecommerce.Apis.Customers.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

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
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = new Error("InternalError", exception?.Message);

            var result = JsonSerializer.Serialize(error);

            await response.WriteAsync(result);
        }
    }
}

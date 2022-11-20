using ecommerce.Apis.Customers.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace ecommerce.Apis.Customers.Extensions;

public static class ExceptionExtensions
{
    public static IApplicationBuilder UseException(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionMiddleware>();
        return builder;
    }
}

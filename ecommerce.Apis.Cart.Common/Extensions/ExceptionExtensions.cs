using ecommerce.Apis.Common.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace ecommerce.Apis.Common.Extensions;

public static class ExceptionExtensions
{
    public static IApplicationBuilder UseException(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionMiddleware>();
        return builder;
    }
}

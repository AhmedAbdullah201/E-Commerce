using E_Commerce.Middlewares;

namespace E_Commerce.Extensions
{
    public static class CustomExceptionHandlingMiddlwareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandlingMiddlware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}

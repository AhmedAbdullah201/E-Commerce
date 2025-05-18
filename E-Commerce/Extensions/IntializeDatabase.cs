using Domain.Contracts;

namespace E_Commerce.Extensions
{
    public static class IntializeDatabase
    {
        public async static Task<WebApplication> IntializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbIntializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbIntializer.InitializeAsync();
            await dbIntializer.InitializeIdentityAsync();

            return app;
        }
    }
}

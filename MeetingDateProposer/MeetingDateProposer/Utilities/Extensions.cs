using MeetingDateProposer.DataLayer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingDateProposer.Utilities
{
    public static class Extensions
    {
        public static void InitializeDbAndSeed(this IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
                dbInitializer.Initialize();
                dbInitializer.Seed();
            }
        }
    }
}
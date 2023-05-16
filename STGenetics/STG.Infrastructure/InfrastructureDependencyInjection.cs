using Microsoft.EntityFrameworkCore;
using STG.Infrastructure.Database;
using STG.Infrastructure.Databse;

namespace Microsoft.Extensions.DependencyInjection.Extensions
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayerDependency(this IServiceCollection services)
        {

            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("STGeneticsDB"));
            services.AddDatabaseDeveloperPageExceptionFilter();




            return services;
        }

        public static async Task SeedDatabase(this IServiceCollection servicescol, IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<AppDbContext>();

                await AppDbSeed.Init(context);
            }
        }

    }
}

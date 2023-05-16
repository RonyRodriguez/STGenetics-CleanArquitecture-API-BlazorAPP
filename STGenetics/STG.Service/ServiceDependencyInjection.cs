using AutoMapper;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection.Extensions
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddServiceLayerDependency(this IServiceCollection services)
        {
            services.AddMapper();

            return services;
        }

        private static void AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                var assembly = Assembly.GetExecutingAssembly();
                var profiles = assembly.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t));
                foreach (var profileType in profiles)
                {
                    cfg.AddProfile(Activator.CreateInstance(profileType) as Profile);
                }
            });
        }


    }
}

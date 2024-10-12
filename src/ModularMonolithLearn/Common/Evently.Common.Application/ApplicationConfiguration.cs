using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Evently.Common.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            Assembly[] moduleAssemblies)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(moduleAssemblies);
            });

            services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);

            return services;
        }
    }
}

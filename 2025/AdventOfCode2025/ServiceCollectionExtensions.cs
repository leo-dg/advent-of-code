using AdventOfCode2025.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode2025
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IReaderService, ReaderService>();
            services.AddScoped<Day1.Day1>();

            return services;
        }
    }
}

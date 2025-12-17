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
            services.AddScoped<Day2.Day2>();
            services.AddScoped<Day3.Day3>();
            services.AddScoped<Day4.Day4>();

            return services;
        }
    }
}

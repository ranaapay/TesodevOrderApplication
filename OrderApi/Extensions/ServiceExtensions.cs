using LoggerService;
using Microsoft.Extensions.DependencyInjection;

namespace OrderApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();

    }
}
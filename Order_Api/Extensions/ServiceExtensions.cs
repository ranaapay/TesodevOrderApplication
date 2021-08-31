using LoggerService;
using Microsoft.Extensions.DependencyInjection;

namespace Order_Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();

    }
}
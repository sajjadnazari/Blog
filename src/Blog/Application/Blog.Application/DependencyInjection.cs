using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // این کد را پیدا کن و تغییرش بده
            services.AddMediator(options =>
            {
                // این خط کلید حل مشکل است! طول عمر هندلرها را هم‌عمر دیتابیس می‌کنیم
                options.ServiceLifetime = ServiceLifetime.Scoped;
            });
            return services;
        }
    }
}

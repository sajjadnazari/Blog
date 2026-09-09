using Blog.Application.Interfaces;
using Blog.Infrastructure.Data.Configurations;
using Blog.Infrastructure.Data.Repositories;
using Blog.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MongoDB.Driver;

namespace Blog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMongoInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // ۱. اجرای تنظیمات مپینگ (جادوی معماری تمیز)
            // این متد به مونگو یاد می‌دهد که چطور کلاس Article را ذخیره کند، بدون اینکه به اتریبیوت نیاز داشته باشیم
            MongoMappingConfig.RegisterMappings();

            // ۲. خواندن رشته اتصال از فایل appsettings.json
            var connectionString = configuration.GetConnectionString("MongoConnection");

            // یک بررسی امنیتی کوچک: اگر یادمان رفته بود کانکشن را در فایل تنظیمات بنویسیم، برنامه همان اول با یک خطای واضح متوقف شود
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "رشته اتصال دیتابیس مونگو در فایل appsettings.json یافت نشد!");
            }

            // ۳. معرفی کلاینت مونگو به عنوان Singleton (راز پرفورمنس)
            // در MongoDB، کلاینت باید حتما Singleton باشد تا اتصالات به دیتابیس (Connection Pool) را مدیریت کند
            services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));

            // ۴. انتخاب دیتابیس مایکروسرویس بلاگ
            services.AddScoped<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();

                // نام دیتابیس را اینجا می‌دهیم. 
                // زیبایی مونگو این است که اگر دیتابیس BlogSystemDb وجود نداشته باشد، نیازی به ساخت دستی نیست؛ 
                // با ثبت اولین مقاله، مونگو خودش آن را خلق می‌کند!
                return client.GetDatabase("BlogSystemDb");
            });

            // ۵. تزریق انبار داده (Repository) به قرارداد (Interface)
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleQueryService, ArticleQueryService>();

            return services;
        }
    }
}

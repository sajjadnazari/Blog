using Blog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// این خط تمام تنظیمات دیتابیس مونگو و Repositoryها رو لود می‌کنه
builder.Services.AddMongoInfrastructure(builder.Configuration);

var app = builder.Build();
app.MapControllers();
app.Run();

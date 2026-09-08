using Blog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ۱. تزریق تنظیمات لایه زیرساخت (MongoDB)
builder.Services.AddMongoInfrastructure(builder.Configuration);

// ۲. راه‌اندازی MediatR با معرفی اسمبلی لایه Application
builder.Services.AddMediator(options =>
{
    options.ServiceLifetime = ServiceLifetime.Scoped;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
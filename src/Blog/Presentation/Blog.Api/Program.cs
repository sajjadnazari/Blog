using Blog.Api.Consumers;
using Blog.Infrastructure;
using Blog.Application;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ۱. تزریق تنظیمات لایه زیرساخت (MongoDB)
builder.Services.AddMongoInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
// ۲. راه‌اندازی MediatR با معرفی اسمبلی لایه Application

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<BusinessCreatedConsumer>(); // معرفی گیرنده

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        // ساخت یک صف اختصاصی برای بلاگ
        cfg.ReceiveEndpoint("blog-business-created-queue", e =>
        {
            e.ConfigureConsumer<BusinessCreatedConsumer>(context);
        });
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
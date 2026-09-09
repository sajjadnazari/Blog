using Blog.Application.Features.Articles.Commands.CreateArticle;
using Contracts.Events;
using MassTransit;
using Mediator;

namespace Blog.Api.Consumers
{
    public class BusinessCreatedConsumer(IMediator mediator) : IConsumer<BusinessCreatedEvent>
    {
        public async Task Consume(ConsumeContext<BusinessCreatedEvent> context)
        {
            // تبدیل رویداد بیرونی به یک دستور داخلی (Command)
            var command = new CreateArticleCommand(
                context.Message.BusinessId,
                $"به بلاگ {context.Message.BusinessTitle} خوش آمدید!",
                "این اولین پست بلاگ شماست."
            );

            // ارسال به لایه Application توسط Mediator
            await mediator.Send(command, context.CancellationToken);
        }
    }
}

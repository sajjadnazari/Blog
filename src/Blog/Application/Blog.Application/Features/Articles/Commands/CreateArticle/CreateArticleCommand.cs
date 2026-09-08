using Mediator;

namespace Blog.Application.Features.Articles.Commands.CreateArticle
{
    public record CreateArticleCommand(Guid BusinessId, string Title, string Content) : IRequest<Guid>;
}

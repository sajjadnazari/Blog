using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using Mediator;

namespace Blog.Application.Features.Articles.Commands.CreateArticle
{
    public sealed class CreateArticleCommandHandler(IArticleRepository articleRepository) : IRequestHandler<CreateArticleCommand, Guid>
    {
        public async ValueTask<Guid> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = Article.Create(request.BusinessId, request.Title, request.Content);
            await articleRepository.AddAsync(article, cancellationToken);
            return article.Id;
        }
    }
}

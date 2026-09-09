using Mediator;

namespace Blog.Application.Features.Articles.Queries.GetArticlesByBusiness
{
    public record GetArticlesByBusinessQuery(Guid BusinessId) : IQuery<List<ArticleDto>>;
}

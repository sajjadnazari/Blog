using Blog.Application.Features.Articles.Queries.GetArticlesByBusiness;

namespace Blog.Application.Interfaces
{
    public interface IArticleQueryService
    {
        ValueTask<List<ArticleDto>> GetArticlesByBusinessIdAsync(Guid businessId, CancellationToken cancellationToken);
    }
}

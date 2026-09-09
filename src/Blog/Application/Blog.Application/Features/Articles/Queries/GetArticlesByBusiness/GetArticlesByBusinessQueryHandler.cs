using Blog.Application.Interfaces;
using Mediator;

namespace Blog.Application.Features.Articles.Queries.GetArticlesByBusiness
{
    public sealed class GetArticlesByBusinessQueryHandler(IArticleQueryService queryService): IQueryHandler<GetArticlesByBusinessQuery, List<ArticleDto>>
    {
        public async ValueTask<List<ArticleDto>> Handle(GetArticlesByBusinessQuery request, CancellationToken cancellationToken)
        {
            // فقط درخواست را به سرویس اختصاصی خواندن پاس می‌دهیم
            return await queryService.GetArticlesByBusinessIdAsync(request.BusinessId, cancellationToken);
        }
    }
}

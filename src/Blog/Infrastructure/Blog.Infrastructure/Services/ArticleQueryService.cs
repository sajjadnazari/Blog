using Blog.Application.Features.Articles.Queries.GetArticlesByBusiness;
using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using MongoDB.Driver;

namespace Blog.Infrastructure.Services
{
    internal sealed class ArticleQueryService(IMongoDatabase database) : IArticleQueryService
    {
        private readonly IMongoCollection<Article> _articles = database.GetCollection<Article>("Articles");

        public async ValueTask<List<ArticleDto>> GetArticlesByBusinessIdAsync(Guid businessId, CancellationToken cancellationToken)
        {
            // ۱. جستجوی مقالات بر اساس BusinessId
            var articles = await _articles
                .Find(x => x.BusinessId == businessId)
                .ToListAsync(cancellationToken);

            // ۲. تبدیل به DTO
            return articles.Select(a => new ArticleDto(
                a.Id,
                a.Title,
                a.Content,
                a.Tags.ToList(),
                a.Comments.Select(c => new CommentDto(c.AuthorName, c.Text, c.CreatedAt)).ToList(),
                a.PublishedAt
            )).ToList();
        }
    }
}

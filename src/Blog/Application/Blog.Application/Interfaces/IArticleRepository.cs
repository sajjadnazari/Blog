using Blog.Domain.Entities;

namespace Blog.Application.Interfaces
{
    public interface IArticleRepository
    {
        Task AddAsync(Article article, CancellationToken cancellationToken = default);
        Task<Article?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}

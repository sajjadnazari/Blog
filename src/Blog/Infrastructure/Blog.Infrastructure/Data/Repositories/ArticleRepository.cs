using Blog.Application.Interfaces;
using Blog.Domain.Entities;
using MongoDB.Driver;

namespace Blog.Infrastructure.Data.Repositories
{
    public sealed class ArticleRepository : IArticleRepository
    {
        // نماینده‌ی جدول (Collection) ما در مونگو
        private readonly IMongoCollection<Article> _articles;

        public ArticleRepository(IMongoDatabase database)
        {
            // در MongoDB جداول رو با عنوان Collection می‌شناسیم
            // متد GetCollection فوق‌العاده هوشمنده: اگر کالکشن Articles وجود نداشته باشه، 
            // خطا نمی‌ده! بلکه به محض اینکه اولین دیتا رو خواستی ثبت کنی، خودش اون رو می‌سازه.
            _articles = database.GetCollection<Article>("Articles");
        }

        public async Task AddAsync(Article article, CancellationToken cancellationToken = default)
        {
            // InsertOneAsync معادل همون AddAsync و SaveChanges در Entity Framework هست
            // با این تفاوت که مونگو درجا توی همون خط دیتا رو توی دیتابیس ذخیره می‌کنه 
            await _articles.InsertOneAsync(article, cancellationToken: cancellationToken);
        }

        public async Task<Article?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // جستجوی به شدت سریع در مونگو:
            // به جای Where از Find استفاده می‌کنیم و اولین نتیجه رو برمی‌گردونیم
            return await _articles.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}

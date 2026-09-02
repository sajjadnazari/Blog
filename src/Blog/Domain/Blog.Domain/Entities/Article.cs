namespace Blog.Domain.Entities
{
    public record Comment(string AuthorName, string Text, DateTime CreatedAt);

    // ریشه تجمیعی (Aggregate Root) مقاله
    public class Article
    {
        // از همان Guid استاندارد استفاده می‌کنیم
        public Guid Id { get; private set; }
        public Guid BusinessId { get; private set; } // ارتباط نرم (Soft Link) با کسب‌وکار نویسنده مقاله
        public string Title { get; private set; }
        public string Content { get; private set; }

        // در مونگو، لیست‌ها دقیقاً درون خود داکیومنت ذخیره می‌شوند
        private readonly List<string> _tags = new();
        public IReadOnlyCollection<string> Tags => _tags.AsReadOnly();

        private readonly List<Comment> _comments = new();
        public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

        public DateTime PublishedAt { get; private set; }

        private Article() { }

        public static Article Create(Guid businessId, string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("عنوان الزامی است");

            return new Article
            {
                Id = Guid.NewGuid(),
                BusinessId = businessId,
                Title = title,
                Content = content,
                PublishedAt = DateTime.UtcNow
            };
        }

        // متدهای رفتاری
        public void AddTag(string tag)
        {
            if (!_tags.Contains(tag)) _tags.Add(tag);
        }

        public void AddComment(string author, string text)
        {
            _comments.Add(new Comment(author, text, DateTime.UtcNow));
        }
    }
}

namespace Blog.Application.Features.Articles.Queries.GetArticlesByBusiness
{
    public record CommentDto(string AuthorName, string Text, DateTime CreatedAt);
}
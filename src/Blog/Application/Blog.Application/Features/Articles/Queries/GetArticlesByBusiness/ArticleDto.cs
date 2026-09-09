using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Features.Articles.Queries.GetArticlesByBusiness
{
    public record ArticleDto(
    Guid Id,
    string Title,
    string Content,
    List<string> Tags,
    List<CommentDto> Comments,
    DateTime PublishedAt
);
}

using Blog.Application.Features.Articles.Commands.CreateArticle;
using Blog.Application.Features.Articles.Queries.GetArticlesByBusiness;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleCommand command, CancellationToken cancellationToken)
        {
            var articleId = await mediator.Send(command, cancellationToken);

            return Created("", new { Id = articleId, Message = "مقاله با موفقیت در بلاگ ثبت شد." });
        }

        [HttpGet("business/{businessId}")]
        public async Task<IActionResult> GetArticlesByBusiness(Guid businessId, CancellationToken cancellationToken)
        {
            var query = new GetArticlesByBusinessQuery(businessId);

            // ارسال درخواست به Mediator
            var articles = await mediator.Send(query, cancellationToken);

            return Ok(articles);
        }
    }
}

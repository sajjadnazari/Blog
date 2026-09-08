using Blog.Application.Features.Articles.Commands.CreateArticle;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API is working!");
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleCommand command, CancellationToken cancellationToken)
        {
            // نحوه ارسال دقیقاً مثل قبل است
            var articleId = await mediator.Send(command, cancellationToken);

            return Created("", new { Id = articleId, Message = "مقاله با موفقیت در بلاگ ثبت شد." });
        }
    }
}

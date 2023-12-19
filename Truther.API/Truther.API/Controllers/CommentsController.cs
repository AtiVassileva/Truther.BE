using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truther.API.Models;

namespace Truther.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly TrutherContext _dbContext;

        public CommentsController(TrutherContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{postId}/Comments")]
        public async Task<IActionResult> GetCommentsByPost(int postId)
        {
            var comments = await _dbContext.Comments
                .Where(c => c.PostId == postId)
                .ToListAsync();
            return Ok(comments);
        }
    }
}
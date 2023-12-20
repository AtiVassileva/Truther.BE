using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truther.API.Infrastructure;
using Truther.API.Models;
using Truther.API.Models.RequestModels;
using static Truther.API.Common.ValidationConstants;

namespace Truther.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly TrutherContext _dbContext;
        private readonly UserExtensions _userExtensions;

        public CommentsController(TrutherContext dbContext, UserExtensions userExtensions)
        {
            _dbContext = dbContext;
            _userExtensions = userExtensions;
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetCommentsByPost(int postId)
        {
            var comments = await _dbContext.Comments
                .Where(c => c.PostId == postId)
                .ToListAsync();
            return Ok(comments);
        }

        [HttpPost("{postId}")]
        public async Task<IActionResult> Post(int postId, CommentCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ContentLengthErrorMessage);
            }


            return Ok();
        }
    }
}
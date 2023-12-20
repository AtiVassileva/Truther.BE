using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truther.API.Infrastructure;
using Truther.API.Models;
using Truther.API.Models.RequestModels;
using static Truther.API.Common.ValidationConstants;
using static Truther.API.Common.AlertMessages;

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

            var currentUserId = await _userExtensions.GetCurrentUserId();

            if (currentUserId == -1)
            {
                return Unauthorized();
            }

            var newComment = new Comment
            {
                Content = model.Content,
                PostId = postId,
                UserId = currentUserId
            };

            await _dbContext.Comments.AddAsync(newComment);
            await _dbContext.SaveChangesAsync();

            return Ok(newComment.Id);
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> Edit(int commentId, CommentCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ContentLengthErrorMessage);
            }

            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
            {
                return BadRequest(NonExistingCommentMsg);
            }

            comment.Content = model.Content;
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
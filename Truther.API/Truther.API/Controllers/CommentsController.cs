using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truther.API.Infrastructure;
using Truther.API.Models;
using Truther.API.Models.RequestModels;
using static Truther.API.Common.AlertMessages;
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

            var currentUserId = await _userExtensions.GetCurrentUserId();

            if (currentUserId == -1)
            {
                return Unauthorized(UnauthorizedToPerformMsg);
            }

            var newComment = new Comment
            {
                Content = model.Content,
                PostId = postId,
                UserId = currentUserId
            };

            await _dbContext.Comments.AddAsync(newComment);
            await _dbContext.SaveChangesAsync();

            return Ok(PostedCommentMsg);
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> Edit(int commentId, CommentCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ContentLengthErrorMessage);
            }

            var comment = await GetCommentById(commentId);

            if (comment == null)
            {
                return NotFound(NonExistingCommentMsg);
            }

            var currentUserId = await _userExtensions.GetCurrentUserId();

            if (currentUserId == -1 || currentUserId != comment.UserId)
            {
                return Unauthorized(UnauthorizedToPerformMsg);
            }

            comment.Content = model.Content;
            await _dbContext.SaveChangesAsync();
            return Ok(EditedCommentMsg);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> Delete(int commentId)
        {
            var comment = await GetCommentById(commentId);

            if (comment == null)
            {
                return NotFound(NonExistingCommentMsg);
            }

            var currentUserId = await _userExtensions.GetCurrentUserId();

            if (currentUserId == -1 || currentUserId != comment.UserId)
            {
                return Unauthorized(UnauthorizedToPerformMsg);
            }

            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();
            return Ok(DeletedCommentMsg);
        }

        private async Task<Comment?> GetCommentById(int commentId)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            return comment;
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truther.API.Infrastructure;
using Truther.API.Models;
using static Truther.API.Common.AlertMessages;

namespace Truther.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharesController : ControllerBase
    {
        private readonly TrutherContext _dbContext;
        private readonly UserExtensions _userExtensions;

        public SharesController(TrutherContext dbContext, UserExtensions userExtensions)
        {
            _dbContext = dbContext;
            _userExtensions = userExtensions;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetSharesByUser(int userId)
        {
            var shares = await _dbContext.Shares
                .Where(sh => sh.UserId == userId)
                .ToListAsync();
            return Ok(shares);
        }

        [HttpPost("{postId}")]
        public async Task<IActionResult> Create(int postId)
        {
            var currentUserId = await _userExtensions.GetCurrentUserId();

            if (currentUserId == -1)
            {
                return Unauthorized(UnauthorizedToPerformMsg);
            }

            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null)
            {
                return NotFound(NonExistingPostMsg);
            }

            var newShare = new Share
            {
                UserId = currentUserId,
                PostId = postId
            };

            await _dbContext.Shares.AddAsync(newShare);
            await _dbContext.SaveChangesAsync();

            return Ok(SuccessfulShareMsg);
        }

        [HttpDelete("{shareId}")]
        public async Task<IActionResult> Delete(int shareId)
        {
            var share = await _dbContext.Shares.FirstOrDefaultAsync(sh => sh.Id == shareId);

            if (share == null)
            {
                return NotFound(NonExistingShareMsg);
            }

            var currentUserId = await _userExtensions.GetCurrentUserId();

            if (currentUserId == -1 || currentUserId != share.UserId)
            {
                return Unauthorized(UnauthorizedToPerformMsg);
            }

            _dbContext.Shares.Remove(share);
            await _dbContext.SaveChangesAsync();
            return Ok(DeletedShareMsg);
        }
    }
}
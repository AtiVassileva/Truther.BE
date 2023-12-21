using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truther.API.Models;

namespace Truther.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharesController : ControllerBase
    {
        private readonly TrutherContext _dbContext;

        public SharesController(TrutherContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetSharesByUser(int userId)
        {
            var shares = await _dbContext.Shares
                .Where(sh => sh.UserId == userId)
                .ToListAsync();
            return Ok(shares);
        }
    }
}
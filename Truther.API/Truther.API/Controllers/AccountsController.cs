using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truther.API.Models;

namespace Truther.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // This controller is responsible for registrations and login operations.
    public class AccountsController : ControllerBase
    {
        private readonly TrutherContext _dbContext;

        public AccountsController(TrutherContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return BadRequest("Not implemented yet.");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string email, string password, string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid registration data!");
            }

            var isUsernameTaken = await _dbContext.Users.AnyAsync(u => u.Username == username);

            if (isUsernameTaken)
            {
                return BadRequest("Username already taken!");
            }

            var newUser = new User
            {
                Email = email,
                Password = password,
                Username = username
            };

            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return Ok("Successful registration!");
        }
    }
}

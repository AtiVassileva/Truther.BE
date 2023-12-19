using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truther.API.Models;
using Truther.API.Models.RequestModels;

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
        public async Task<IActionResult> GetAll()
        {
            var accountsList = await _dbContext.Users.ToListAsync();
            return Ok(accountsList);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid registration data!");
            }

            var isUsernameTaken = await _dbContext.Users.AnyAsync(u => u.Username == model.Username);

            if (isUsernameTaken)
            {
                return BadRequest("Username already taken!");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var newUser = new User
            {
                Email = model.Email,
                Password = hashedPassword,
                Username = model.Username
            };

            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return Ok("Successful registration!");
        }
    }
}

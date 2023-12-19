using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truther.API.Models;
using Truther.API.Models.RequestModels;
using static Truther.API.Common.AlertMessages;

namespace Truther.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // This controller is responsible for registrations and login operations.
    public class AccountsController : ControllerBase
    {
        private readonly TrutherContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountsController(TrutherContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accountsList = await _dbContext.Users.ToListAsync();
            return Ok(accountsList);
        }

        [HttpGet("CurrentUser")]
        public string? GetCurrentUserLoginToken()
        {
            var loginToken = _httpContextAccessor.HttpContext!.Session.GetString(LoginTokenKey);
            return loginToken;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(InvalidRegistrationDataMsg);
            }

            var isUsernameTaken = await _dbContext.Users.AnyAsync(u => u.Username == model.Username);

            if (isUsernameTaken)
            {
                return BadRequest(UsernameTakenMsg);
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

            return Ok(SuccessfulRegistrationMsg);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(InvalidLoginAttemptMsg);
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user == null)
            {
                return BadRequest(InvalidUsernameMsg);
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

            if (!isPasswordValid)
            {
                return BadRequest(WrongPasswordMsg);
            }

            var loginToken = $"{model.Username}_{DateTime.Now.ToString(CultureInfo.InvariantCulture)}";
            _httpContextAccessor.HttpContext!.Session.SetString(LoginTokenKey, loginToken);

            return Ok(SuccessfullyLoggedInMsg);
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            var currentUserToken = GetCurrentUserLoginToken();

            if (string.IsNullOrEmpty(currentUserToken))
            {
                return BadRequest(InvalidLogoutAttempt);
            }

            _httpContextAccessor.HttpContext!.Session.SetString(LoginTokenKey, string.Empty);
            return Ok(SuccessfullyLoggedOutMsg);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Truther.API.Models;
using static Truther.API.Common.AlertMessages;

namespace Truther.API.Infrastructure
{
    public class UserExtensions
    {
        private readonly TrutherContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserExtensions(TrutherContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> GetCurrentUserId()
        {
            if (!IsAuthenticated())
            {
                return Guid.Empty;
            }

            var loginToken = GetCurrentUserLoginToken()!;
            var username = loginToken.Split("_")[0];
            var currentUser = await _dbContext.Users.FirstAsync(u => u.Username == username);

            return currentUser.Id;
        }

        public bool IsAuthenticated()
        {
            var currentUserLoginToken = GetCurrentUserLoginToken();
            return !string.IsNullOrEmpty(currentUserLoginToken);
        }

        public string? GetCurrentUserLoginToken()
        {
            var loginToken = _httpContextAccessor.HttpContext!.Session.GetString(LoginTokenKey);
            return loginToken;
        }
    }
}

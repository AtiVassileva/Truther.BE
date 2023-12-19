using System.ComponentModel.DataAnnotations;
using static Truther.API.Common.ValidationConstants;

namespace Truther.API.Models.RequestModels
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = RequiredUsernameErrorMessage)]
        [MinLength(DefaultMinLength, ErrorMessage = UsernameMinLengthError)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = RequiredPasswordErrorMessage)]
        [MinLength(PasswordMinLength, ErrorMessage = PasswordMinLengthError)]
        public string Password { get; set; } = null!;
    }
}
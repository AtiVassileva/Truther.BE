using System.ComponentModel.DataAnnotations;
using static Truther.API.Common.ValidationConstants;

namespace Truther.API.Models.RequestModels
{
    public class UserRegisterModel : UserLoginModel
    {
        [Required(ErrorMessage = RequiredEmailErrorMessage)]
        [EmailAddress(ErrorMessage = InvalidEmailErrorMessage)]
        public string Email { get; set; } = null!;
    }
}
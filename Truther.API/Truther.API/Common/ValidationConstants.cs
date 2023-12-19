namespace Truther.API.Common
{
    public class ValidationConstants
    {
        public const int DefaultMinLength = 3;
        public const int PasswordMinLength = 8;

        public const string RequiredEmailErrorMessage = "Email address cannot be empty!";
        public const string InvalidEmailErrorMessage = "Invalid email address!";

        public const string RequiredUsernameErrorMessage = "Username cannot be empty!";
        public const string UsernameMinLengthError = "Username should contain at least 3 characters!";

        public const string RequiredPasswordErrorMessage = "Password cannot be empty!";
        public const string PasswordMinLengthError = "Password should contain at least 8 characters!";
    }
}
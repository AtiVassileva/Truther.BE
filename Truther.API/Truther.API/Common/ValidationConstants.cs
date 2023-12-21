namespace Truther.API.Common
{
    public class ValidationConstants
    {
        public const int DefaultMinLength = 3;
        public const int PasswordMinLength = 8;
        public const int ContentMaxLength = 2000;

        public const string RequiredEmailErrorMessage = "Email address cannot be empty!";
        public const string InvalidEmailErrorMessage = "Invalid email address!";

        public const string RequiredUsernameErrorMessage = "Username cannot be empty!";
        public const string UsernameMinLengthErrorMessage = "Username should contain at least 3 characters!";

        public const string RequiredPasswordErrorMessage = "Password cannot be empty!";
        public const string PasswordMinLengthErrorMessage = "Password should contain at least 8 characters!";

        public const string ContentLengthErrorMessage = "Content should contain between 3 and 2000 characters!";
    }
}
namespace Truther.API.Common
{
    public class AlertMessages
    {
        public const string LoginTokenKey = "LoginToken";

        public const string InvalidRegistrationDataMsg = "Invalid registration data!";
        public const string UnauthorizedToPerformMsg = "Error! You are unauthorized to perform this action!";
        public const string UsernameTakenMsg = "Username already taken!";
        public const string SuccessfulRegistrationMsg = "Successful registration!";

        public const string InvalidLoginAttemptMsg = "Invalid login attempt!";
        public const string InvalidUsernameMsg = "Invalid username!";
        public const string WrongPasswordMsg = "Wrong password!";
        public const string SuccessfullyLoggedInMsg = "Successfully logged in!";

        public const string InvalidLogoutAttempt = "Invalid logout attempt!";
        public const string SuccessfullyLoggedOutMsg = "Successfully logged out!";

        public const string NonExistingCommentMsg = "Comment does not exist!";
        public const string PostedCommentMsg = "Comment posted successfully!";
        public const string EditedCommentMsg = "Comment edited successfully!";
        public const string DeletedCommentMsg = "Comment deleted!";

        public const string ErrorOnLike = "Like count was not update propperly on like!";
        public const string NonExistingPost = "Post was not found!";
    }
}
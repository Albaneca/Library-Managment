using System;

namespace Library.Common
{
    public static class GlobalConstants
    {
        public const string Secret = "SomeLongerJWTSecretKeyCuzItWasBroken";
        //TODO
        public const string Domain = "http://localhost:5001/";

        public const string DefaultPicture = "https://res.cloudinary.com/denfz6l1q/image/upload/v1639012542/profilePictures/oymjlgd6v2yi9dm3pynn.jpg";

        public const string AdministratorRoleName = "Admin";

        public const string UserRoleName = "User";

        public const string BannedRoleName = "Banned";

        public const string NotConfirmedRoleName = "NotConfirmed";

        public const string PassRegex = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$";

        public const string PhoneRegex = @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";

        public const int PageSkip = 10;

        public const string VALUE_LENGTH_ERROR = "Value for {0} must be between {2} and {1}.";

        public const string PASSWORD_ERROR_MESSAGE = "Password must be at least 8 symbols and should contain capital letter, digit and special symbol (+, -, *, &, ^, …)";

        public const string PASSWORDS_MUST_MATCH = "Password and Confirmation Password must match.";

        public const string PLEASE_CONFIRM_PASS = "Please confirm your password (Inside security tab).";

        public const string OLD_PASSWORD = "Old password is wrong.";

        public const string INVALID_EMAIL = "Invalid Email Address!";

        public const string LOAN_NOT_CONFIRMED = "Waiting";

        public const string INCORRECT_DATA = "Incorrect user data. Try again!";

        public const string USER_NOT_FOUND = "User not found";

        public const string USER_EXISTS = "User with this email already exist. Try another!";

        public const string USER_PHONE_EXISTS = "User with this phone number already exist. Try another!";

        public const string WRONG_CREDENTIALS = "Incorrect password";

        public const string NOT_AUTHORIZED = "User is not authorized.";

        public const string LOGGED = "Succesfully logged";

        public const string USER_BLOCKED_JOIN = "User is currently banned and cannot join the system!";

        public const string USERNAME_EXIST = "This username is already used, try another.";
        public const string USER_UNBLOCKED = "User was successfully unblocked";
        public const string NO_COMMENT = "No reason provided for this ban";
        public const string AUTHOR_NOT_FOUND = "This author does not exist!";
        public const string AUTHOR_EXISTS = "Author with this name already exist";
        public const string BOOK_NOT_FOUND = "Book not found!";
        public const string BOOK_EXISTS = "Book with this name already exist";
        public const string HOUSE_NOT_FOUND = "Publish house not found";
        public const string HOUSE_EXIST = "Publish house with this name already exist";
        public const string DefaultPublishHouseURL = "https://res.cloudinary.com/denfz6l1q/image/upload/v1645362793/publishHousePictures/Random_House_logo_bw2-1024x819_ptore3.jpg";
        public const string DefaultBookURL = "https://res.cloudinary.com/denfz6l1q/image/upload/v1644718347/booksPictures/leather-book-preview_gyrmg9.png";
        public const string DefaultAuthorURL = "https://res.cloudinary.com/denfz6l1q/image/upload/v1639012542/profilePictures/oymjlgd6v2yi9dm3pynn.jpg";
        public const string LOAN_NOT_FOUND = "Loan not found";
        public const string LOAN_CONFIRMED = "Confirmed";
        public const string LOAN_DENIED = "Denied";
        public const string WRONG_PHONE = "Phone number is incorrect or not full, try again!";
        public static string NOT_PROVIDED = "Phone number was not provided";
    }
}

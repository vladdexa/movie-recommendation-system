using System.Collections.Generic;

namespace UserApi
{
    public enum UserExceptionType
    {
        UserNotFound,
        UserAlreadyExists,
        UserInvalidCredentials,
        UserFirstNameRequired,
        UserLastNameRequired,
        UserEmailRequired,
        UserPasswordRequired,
        UserPasswordTooShort,
        UserPasswordTooLong,
        UserInvalidEmailAddress,
        UserAuthorizationRequestHeadersMissing,
        UserAuthorizationRequestMissingBearer,
        UserAuthorizationRequestInvalidToken
    }

    public static class UserException
    {
        public static readonly Dictionary<UserExceptionType, string> CreateUserExceptions =
            new Dictionary<UserExceptionType, string>
            {
                {UserExceptionType.UserFirstNameRequired, "User firstName is a required field"},
                {UserExceptionType.UserLastNameRequired, "User lastName is a required field"},
                {UserExceptionType.UserEmailRequired, "User email is a required field"},
                {UserExceptionType.UserInvalidEmailAddress, "User email must be a valid email address"},
                {UserExceptionType.UserPasswordRequired, "User firstName is a required field"},
                {UserExceptionType.UserPasswordTooLong, "User password is too short!"},
                {UserExceptionType.UserPasswordTooShort, "User password is too long!"},
                {UserExceptionType.UserAlreadyExists, "User already exists!"}
            };

        public static readonly Dictionary<UserExceptionType, string> AuthorizeUserExceptions =
            new Dictionary<UserExceptionType, string>
            {
                {
                    UserExceptionType.UserAuthorizationRequestHeadersMissing,
                    "Request Headers does not contain Authorization"
                },
                {UserExceptionType.UserInvalidCredentials, "User credentials are invalid"},
                {UserExceptionType.UserNotFound, "User not found"},
                {
                    UserExceptionType.UserAuthorizationRequestMissingBearer,
                    "Missing Bearer from Authorization Request Header"
                },
                {UserExceptionType.UserAuthorizationRequestInvalidToken, "User Token is invalid"}
            };
    }
}
using FluentValidation;

namespace UserApi
{
    public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserRequestValidator()
        {
            RuleFor(loginUserRequest => loginUserRequest.Email).NotEmpty().NotNull()
                .WithMessage(UserException.CreateUserExceptions[UserExceptionType.UserEmailRequired]);

            RuleFor(loginUserRequest => loginUserRequest.Email).Matches(User.EmailPattern)
                .WithMessage(UserException.CreateUserExceptions[UserExceptionType.UserInvalidEmailAddress]);

            RuleFor(loginUserRequest => loginUserRequest.Password).NotEmpty().NotNull()
                .WithMessage(UserException.CreateUserExceptions[UserExceptionType.UserPasswordRequired]);

            RuleFor(loginUserRequest => loginUserRequest.Password).MinimumLength(User.PasswordLowerBoundConstraint)
                .WithMessage(UserException.CreateUserExceptions[UserExceptionType.UserPasswordTooShort]);

            RuleFor(loginUserRequest => loginUserRequest.Password).MaximumLength(User.PasswordUpperBoundConstraint)
                .WithMessage(UserException.CreateUserExceptions[UserExceptionType.UserPasswordTooLong]);
        }
    }

    public class LoginUserRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
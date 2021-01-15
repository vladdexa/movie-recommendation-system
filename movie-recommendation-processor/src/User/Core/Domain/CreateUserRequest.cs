using FluentValidation;

namespace UserApi
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(createUserRequest => createUserRequest.Email).NotEmpty().NotNull()
                .WithMessage(UserException.CreateUserExceptions[UserExceptionType.UserEmailRequired]);

            RuleFor(createUserRequest => createUserRequest.Email).Matches(User.EmailPattern)
                .WithMessage(UserException.CreateUserExceptions[UserExceptionType.UserInvalidEmailAddress]);

            RuleFor(createUserRequest => createUserRequest.Password).NotEmpty().NotNull()
                .WithMessage(UserException.CreateUserExceptions[UserExceptionType.UserPasswordRequired]);

            RuleFor(createUserRequest => createUserRequest.Password).MinimumLength(User.PasswordLowerBoundConstraint)
                .WithMessage(UserException.CreateUserExceptions[UserExceptionType.UserPasswordTooShort]);

            RuleFor(createUserRequest => createUserRequest.Password).MaximumLength(User.PasswordUpperBoundConstraint)
                .WithMessage(UserException.CreateUserExceptions[UserExceptionType.UserPasswordTooLong]);

            RuleFor(createUserRequest => createUserRequest.FirstName).NotEmpty().NotNull()
                .WithMessage(UserException.CreateUserExceptions[UserExceptionType.UserFirstNameRequired]);

            RuleFor(createUserRequest => createUserRequest.LastName).NotEmpty().NotNull()
                .WithMessage(UserException.CreateUserExceptions[UserExceptionType.UserLastNameRequired]);
        }
    }

    public class CreateUserRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
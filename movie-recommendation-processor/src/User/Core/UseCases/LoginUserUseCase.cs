using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using User.Config.Utils;
using User.Utils.PasswordHasher;

namespace UserApi
{
    public class LoginUserUseCase
    {
        public readonly Func<IUserRepository, LoginUserRequest, Task<string>> Execute =
            async (userRepository, loginUserRequest) =>
            {
                var foundUser = (await userRepository.GetUserByEmail(loginUserRequest.Email)).ToUser();

                if (foundUser == null)
                    throw new Exception(UserException.AuthorizeUserExceptions[UserExceptionType.UserNotFound]);

                var hashingOptions = new OptionsWrapper<HashingOptions>(new HashingOptions());

                var passwordHasher = new PasswordHasher(hashingOptions);

                var passwordCheck =
                    passwordHasher.Check(foundUser.Password, loginUserRequest.Password);

                if (!passwordCheck.Verified)
                    throw new Exception(
                        UserException.AuthorizeUserExceptions[UserExceptionType.UserInvalidCredentials]);

                var userInfo = new UserInfo
                {
                    Email = foundUser.Email,
                    FirstName = foundUser.FirstName,
                    LastName = foundUser.LastName,
                    UserId = foundUser.UserId
                };

                return new Jwt().Encode(userInfo);
            };
    }
}
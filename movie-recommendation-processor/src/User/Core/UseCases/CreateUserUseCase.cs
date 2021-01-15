using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using User.Config.Utils;
using User.Utils.PasswordHasher;

namespace UserApi
{
    public class CreateUserUseCase
    {
        public readonly Func<IUserRepository, CreateUserRequest, Task<RegisteredUserDto>> Execute =
            async (userRepository, createUserRequest) =>
            {
                var foundUser = await userRepository.GetUserByEmail(createUserRequest.Email);

                if (foundUser != null)
                    throw new Exception(UserException.CreateUserExceptions[UserExceptionType.UserAlreadyExists]);

                var hashingOptions = new OptionsWrapper<HashingOptions>(new HashingOptions());

                var passwordHasher = new PasswordHasher(hashingOptions);

                var userToCreate = new User
                {
                    Email = createUserRequest.Email,
                    Password = passwordHasher.Hash(createUserRequest.Password),
                    FirstName = createUserRequest.FirstName,
                    LastName = createUserRequest.LastName
                }.ToUserTableEntity();

                var userInfo = (await userRepository.CreateUser(userToCreate)).ToUser();

                var userInfoDto = new UserInfo
                {
                    UserId = userInfo.UserId,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    Email = userInfo.Email
                };

                var userToken = new Jwt().Encode(userInfoDto);

                return new RegisteredUserDto
                {
                    UserInfo = userInfoDto,
                    UserToken = userToken
                };
            };
    }
}
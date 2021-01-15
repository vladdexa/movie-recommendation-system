using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace UserApi
{
    public class GetUserUseCase
    {
        public readonly Func<IUserRepository, HttpRequest, Task<UserInfo>> Execute = async (userRepository, request) =>
        {
            var (_, user) = await new AuthorizeUserUseCase().Execute(userRepository, request);

            return new UserInfo
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = user.UserId
            };
        };
    }
}
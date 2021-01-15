using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace UserApi
{
    public class DeleteUserUseCase
    {
        public readonly Func<IUserRepository, HttpRequest, Task> Execute = async (userRepository, request) =>
        {
            var (_, user) = await new AuthorizeUserUseCase().Execute(userRepository, request);

            await userRepository.DeleteUser(user.ToUserTableEntity());
        };
    }
}
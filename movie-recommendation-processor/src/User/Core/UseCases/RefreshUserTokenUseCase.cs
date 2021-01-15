using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using User.Config.Utils;

namespace UserApi
{
    public class RefreshUserTokenUseCase
    {
        public readonly Func<IUserRepository, HttpRequest, Task<string>> Execute = async (userRepository, request) =>
        {
            var (jwtToken, _) = await new AuthorizeUserUseCase().Execute(userRepository, request);

            //invalidate Token
            jwtToken.ExpirationTime = DateTime.Now.Ticks;

            return new Jwt().Encode(jwtToken.UserInfo);
        };
    }
}
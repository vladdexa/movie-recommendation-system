using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using User.Config.Utils;

namespace UserApi
{
    public class AuthorizeUserUseCase
    {
        private static readonly Func<long, bool> IsTokenExpired =
            expirationTime => DateTime.Now.Ticks >= expirationTime;

        public readonly Func<IUserRepository, HttpRequest, Task<(JwtToken, User)>> Execute =
            async (userRepository, httpRequest) =>
            {
                var jwtToken = new Jwt().Decode(httpRequest);

                if (IsTokenExpired(jwtToken.ExpirationTime))
                    throw new Exception(
                        UserException.AuthorizeUserExceptions[UserExceptionType.UserAuthorizationRequestInvalidToken]);

                var foundUser = (await userRepository.GetUser(jwtToken.UserInfo.UserId)).ToUser();

                if (foundUser.Equals(null)
                    || !foundUser.UserId.Equals(jwtToken.UserInfo.UserId)
                    || !foundUser.Email.Equals(jwtToken.UserInfo.Email)
                    || !foundUser.FirstName.Equals(jwtToken.UserInfo.FirstName)
                    || !foundUser.LastName.Equals(jwtToken.UserInfo.LastName)
                )
                    throw new Exception(
                        UserException.AuthorizeUserExceptions[UserExceptionType.UserAuthorizationRequestInvalidToken]);

                return (jwtToken, foundUser);
            };
    }
}
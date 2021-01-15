using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using UserApi;

namespace User.Functions
{
    public class RefreshUserTokenHandler
    {
        private readonly IUserRepository _userRepository;

        public RefreshUserTokenHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [FunctionName("RefreshUserTokenHandler")]
        public async Task<IActionResult> RefreshUserToken(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "refresh-token")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("Refresh token triggered");

            try
            {
                var userToken = await new RefreshUserTokenUseCase().Execute(_userRepository, req);

                return new ObjectResult(userToken);
            }
            catch (Exception e)
            {
                log.LogInformation(e.Message);

                if (e.Message.Equals(UserException.AuthorizeUserExceptions[UserExceptionType.UserNotFound]))
                    return new BadRequestObjectResult(e.Message);

                if (e.Message.Equals(UserException.AuthorizeUserExceptions[UserExceptionType.UserInvalidCredentials]))
                    return Authorization.Reject(e.Message);

                throw new Exception(e.Message);
            }
        }
    }
}
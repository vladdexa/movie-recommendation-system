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
    public class GetUserHandler
    {
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [FunctionName("GetUserHandler")]
        public async Task<IActionResult> GetUser(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/{userId}")]
            HttpRequest req, ILogger log, string userId)
        {
            log.LogInformation($"Get the user with id={userId}");

            try
            {
                var userInfo = await new GetUserUseCase().Execute(_userRepository, req);

                return new ObjectResult(userInfo);
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
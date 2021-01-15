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
    public class DeleteUserHandler
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [FunctionName("DeleteUserHandler")]
        public async Task<IActionResult> DeleteUser(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "user/{userId}")]
            HttpRequest req, ILogger log, string userId)
        {
            log.LogInformation($"Delete the user with id={userId}");

            try
            {
                await new DeleteUserUseCase().Execute(_userRepository, req);

                return new NoContentResult();
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
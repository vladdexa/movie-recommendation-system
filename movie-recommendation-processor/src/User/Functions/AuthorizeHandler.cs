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
    public class Authorizer
    {
        private readonly IUserRepository _userRepository;

        public Authorizer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [FunctionName("Authorizer")]
        public async Task<IActionResult> Authorize(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "authorizer")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("Authorize the request");

            try
            {
                await new AuthorizeUserUseCase().Execute(_userRepository, req);

                return Authorization.Authorize();
            }
            catch (Exception e)
            {
                log.LogInformation(e.Message);

                return Authorization.Reject(e.Message);
            }
        }
    }
}
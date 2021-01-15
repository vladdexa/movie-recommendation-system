using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UserApi;

namespace User.Functions
{
    public class LoginUserHandler
    {
        private readonly IUserRepository _userRepository;

        public LoginUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [FunctionName("LoginUserHandler")]
        public async Task<IActionResult> LoginUser(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "login-user")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("Login a  User");

            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var loginUserRequest = JsonConvert.DeserializeObject<LoginUserRequest>(requestBody);

                var validationResult = await new LoginUserRequestValidator().ValidateAsync(loginUserRequest);

                if (!validationResult.IsValid)
                    return new BadRequestObjectResult(validationResult.Errors.Select(e => new
                    {
                        Field = e.PropertyName,
                        Error = e.ErrorMessage
                    }));

                var token = await new LoginUserUseCase().Execute(_userRepository, loginUserRequest);

                log.LogInformation("User has been logged in successfully");

                return new ObjectResult(token);
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
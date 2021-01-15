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
    public class CreateUserHandler
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [FunctionName("CreateUserHandler")]
        public async Task<IActionResult> CreateUser(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "create-user")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("Creating a new User");

            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var createUserRequest = JsonConvert.DeserializeObject<CreateUserRequest>(requestBody);

                var validationResult = await new CreateUserRequestValidator().ValidateAsync(createUserRequest);

                if (!validationResult.IsValid)
                    return new BadRequestObjectResult(validationResult.Errors.Select(e => new
                    {
                        Field = e.PropertyName,
                        Error = e.ErrorMessage
                    }));

                var user = await new CreateUserUseCase().Execute(_userRepository, createUserRequest);

                log.LogInformation("User has been created successfully");

                return new OkObjectResult(user);
            }
            catch (Exception e)
            {
                log.LogInformation(e.Message);

                if (e.Message.Equals(UserException.CreateUserExceptions[UserExceptionType.UserAlreadyExists]))
                    return new BadRequestObjectResult(e.Message);

                throw new Exception(e.Message);
            }
        }
    }
}
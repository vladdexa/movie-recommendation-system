using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MovieApi;
using Newtonsoft.Json;

namespace Movie.Functions
{
    public class CreateMovieHandler
    {
        private readonly IMovieRepository _movieRepository;

        public CreateMovieHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [FunctionName("CreateMovieHandler")]
        public async Task<IActionResult> CreateMovie(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "create-movie")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("Creating a new Movie");

            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var createMovieRequest = JsonConvert.DeserializeObject<CreateMovieRequest>(requestBody);

                var validationResult = await new CreateMovieRequestValidator().ValidateAsync(createMovieRequest);

                if (!validationResult.IsValid)
                    return new BadRequestObjectResult(validationResult.Errors.Select(e => new
                    {
                        Field = e.PropertyName,
                        Error = e.ErrorMessage
                    }));

                var movie = await new CreateMovieUseCase().Execute(_movieRepository, createMovieRequest);

                log.LogInformation("Movie created with success");

                return new OkObjectResult(movie);
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}
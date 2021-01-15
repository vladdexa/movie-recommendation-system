using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MovieApi;

namespace Movie.Functions
{
    public class GetMovieHandler
    {
        private readonly IMovieRepository _movieRepository;

        public GetMovieHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [FunctionName("GetMovieHandler")]
        public async Task<IActionResult> GetMovie(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "movie/{id}")]
            HttpRequest req, ILogger log, string id)
        {
            log.LogInformation($"Get the movie with id={id}");

            try
            {
                var movie = await new GetMovieUseCase().Execute(_movieRepository, id);

                log.LogInformation("Movie retrieved with success");

                if (movie == null) return new NotFoundResult();

                return new OkObjectResult(movie);
            }
            catch (Exception e)
            {
                log.LogError(e.Message);

                if (e.Message.Equals(MovieException.MovieExceptions[MovieExceptionType.MovieNotFound]))
                    return new BadRequestObjectResult(e.Message);

                throw new Exception(e.Message);
            }
        }
    }
}
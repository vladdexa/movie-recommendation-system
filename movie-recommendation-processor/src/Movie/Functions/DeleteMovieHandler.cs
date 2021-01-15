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
    public class DeleteMovieHandler
    {
        private readonly IMovieRepository _movieRepository;

        public DeleteMovieHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [FunctionName("DeleteMovieHandler")]
        public async Task<IActionResult> DeleteMovie(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "movie/{id}")]
            HttpRequest req, ILogger log, string id)
        {
            try
            {
                await new DeleteMovieUseCase().Execute(_movieRepository, id);

                return new NoContentResult();
            }
            catch (Exception e)
            {
                log.LogInformation(e.Message);

                if (e.Message.Equals(MovieException.MovieExceptions[MovieExceptionType.MovieNotFound]))
                    return new BadRequestObjectResult(e.Message);

                throw new Exception(e.Message);
            }
        }
    }
}
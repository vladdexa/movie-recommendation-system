using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MovieApi;

namespace Movie.Functions
{
    public class GetRatedMoviesForUserHandler
    {
        private readonly IMovieRepository _movieRepository;

        public GetRatedMoviesForUserHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [FunctionName("GetRatedMoviesForUserHandler")]
        public async Task<IActionResult> GetRatedMoviesForUser(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "rated-movies/{userId}")]
            HttpRequest req, ILogger log, string userId)
        {
            try
            {
                return new OkObjectResult(
                    await new GetRatedMoviesForUserGenreUseCase().Execute(_movieRepository, userId));
            }
            catch (Exception error)
            {
                log.LogError(error.Message);
                throw new Exception(error.Message);
            }
        }
    }
}
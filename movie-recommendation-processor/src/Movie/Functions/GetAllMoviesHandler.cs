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
    public class GetAllMoviesHandler
    {
        private readonly IMovieRepository _movieRepository;

        public GetAllMoviesHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [FunctionName("GetAllMoviesHandler")]
        public async Task<IActionResult> GetAllMovies(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "movies")]
            HttpRequest req, ILogger log)
        {
            try
            {
                return new OkObjectResult(await new GetAllMoviesUseCase().Execute(_movieRepository));
            }
            catch (Exception error)
            {
                log.LogError(error.Message);
                throw new Exception(error.Message);
            }
        }
    }
}
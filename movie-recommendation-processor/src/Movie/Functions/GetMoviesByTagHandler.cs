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
    public class GetMoviesByTagHandler
    {
        private readonly IMovieRepository _movieRepository;

        public GetMoviesByTagHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }


        [FunctionName("GetMoviesByTagHandler")]
        public async Task<IActionResult> GetMoviesByTag(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "movies-by-tag/{tag}")]
            HttpRequest req, ILogger log, string tag)
        {
            try
            {
                if (tag == null) return new BadRequestObjectResult("Tag is required");

                return new OkObjectResult(await new GetMoviesByTagUseCase().Execute(_movieRepository, tag));
            }
            catch (Exception error)
            {
                log.LogError(error.Message);
                throw new Exception(error.Message);
            }
        }
    }
}
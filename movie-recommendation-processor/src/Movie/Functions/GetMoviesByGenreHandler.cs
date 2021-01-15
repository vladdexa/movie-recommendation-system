using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MovieApi;

//TODO change to COSMOS DB SQL CORE 
namespace Movie.Functions
{
    public class GetMoviesByGenreHandler
    {
        private readonly IMovieRepository _movieRepository;

        public GetMoviesByGenreHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [FunctionName("GetMoviesByGenreHandler")]
        public async Task<IActionResult> GetMoviesByGenre(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "movies/{genre}")]
            HttpRequest req, ILogger log, string genre)
        {
            try
            {
                var movies = await new GetMoviesByGenreUseCase().Execute(_movieRepository, genre);
                
                return new OkObjectResult(movies);
            }
            catch (Exception error)
            {
                log.LogError(error.Message);
                throw new Exception(error.Message);
            }
        }
    }
}
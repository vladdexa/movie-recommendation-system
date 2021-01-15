using System;
using System.IO;
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
    public class UpdateMovieHandler
    {
        private readonly IMovieRepository _movieRepository;

        public UpdateMovieHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [FunctionName("UpdateMovieHandler")]
        public async Task<IActionResult> UpdateMovie(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "movie/{id}")]
            HttpRequest req, ILogger log, string id)
        {
            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var updateMovieRequest = JsonConvert.DeserializeObject<UpdateMovieRequest>(requestBody);

                if (updateMovieRequest.Tag == null && updateMovieRequest.AlreadySeen == null &&
                    updateMovieRequest.IsFavourite == null && updateMovieRequest.UserRating == null)
                    return new BadRequestObjectResult("Invalid updateMovieRequest");

                var movie = await new UpdateMovieUseCase().Execute(_movieRepository, updateMovieRequest, id);

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
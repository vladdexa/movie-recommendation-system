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
    public class SearchMovieHandler
    {
        private readonly IMovieRepository _movieRepository;

        public SearchMovieHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

#nullable enable
        [FunctionName("SearchMovieHandler")]
        public async Task<IActionResult> SearchMovie(
            [HttpTrigger(AuthorizationLevel.Function, "get",
                Route = "movies/search/from={from}&size={size}&query={query}")]
            HttpRequest req, ILogger log, int from, int size, string? query)
#nullable disable
        {
            try
            {
                SearchMovieRequest searchMovieRequest = SearchMovieRequest.Create(new SearchMovieRequestPayload()
                    {From = from, Size = size, Query = query});

                return new OkObjectResult(await new SearchMovieUseCase().Execute(_movieRepository, searchMovieRequest));
            }
            catch (Exception error)
            {
                log.LogError(error.Message);

                if (error.Message.Equals(
                        MovieException.SearchMovieExceptions[
                            SearchMovieExceptionType.SearchMovieRequestSizeExceeded]) ||
                    error.Message.Equals(
                        MovieException.SearchMovieExceptions[SearchMovieExceptionType.SearchMovieRequestFromWrongInput])
                )
                {
                    return new BadRequestObjectResult(error.Message);
                }

                throw new Exception(error.Message);
            }
        }
    }
}
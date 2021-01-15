using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi
{
    public class SearchMovieUseCase
    {
        public readonly Func<IMovieRepository, SearchMovieRequest, Task<SearchMovieResponse>> Execute =
            async (movieRepository, searchMovieRequest) =>
            {
                var allMovies = await movieRepository.GetAllMovies();

                var results = searchMovieRequest.Query != null
                    ? allMovies
                        .Where(movieTableEntity => movieTableEntity.Name.Contains(searchMovieRequest.Query)).ToList()
                    : allMovies.ToList();

                List<Movie> resultsDto = new List<Movie>();

                foreach (var movieTableEntity in results)
                {
                    resultsDto.Add(movieTableEntity.ToMovie());
                }

                return new SearchMovieResponse()
                {
                    Movies = resultsDto.Skip(searchMovieRequest.From).Take(searchMovieRequest.Size),
                    Total = results.Count
                };
            };
    }
}
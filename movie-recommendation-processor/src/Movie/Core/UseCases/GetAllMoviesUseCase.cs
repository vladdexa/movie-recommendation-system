using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi
{
    public class GetAllMoviesUseCase
    {
        public readonly Func<IMovieRepository, Task<IEnumerable<Movie>>> Execute =
            async movieRepository =>
            {
                var movies = await movieRepository.GetAllMovies();

                List<Movie> moviesDto = new List<Movie>();

                foreach (var movieTableEntity in movies) moviesDto.Add(movieTableEntity.ToMovie());

                return moviesDto;
            };
    }
}
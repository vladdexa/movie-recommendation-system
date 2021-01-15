using System;
using System.Threading.Tasks;

namespace MovieApi
{
    public class GetMovieUseCase
    {
        public readonly Func<IMovieRepository, string, Task<Movie>> Execute =
            async (movieRepository, movieId) =>
            {
                var movie = (await movieRepository.GetMovie(movieId)).ToMovie();

                if (movie.MovieId != movieId)
                    throw new Exception(MovieException.MovieExceptions[MovieExceptionType.MovieNotFound]);

                return movie;
            };
    }
}
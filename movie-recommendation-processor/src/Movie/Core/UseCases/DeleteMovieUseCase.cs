using System;
using System.Threading.Tasks;

namespace MovieApi
{
    public class DeleteMovieUseCase
    {
        public readonly Func<IMovieRepository, string, Task> Execute = async (movieRepository, movieId) =>
        {
            var foundMovie = await new GetMovieUseCase().Execute(movieRepository, movieId);

            if (foundMovie.MovieId != movieId)
                throw new Exception(MovieException.MovieExceptions[MovieExceptionType.MovieNotFound]);

            await movieRepository.DeleteMovie(foundMovie);
        };
    }
}
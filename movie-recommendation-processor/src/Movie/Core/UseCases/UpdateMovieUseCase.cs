using System;
using System.Threading.Tasks;

namespace MovieApi
{
    public class UpdateMovieUseCase
    {
        public readonly Func<IMovieRepository, UpdateMovieRequest, string, Task<Movie>> Execute =
            async (movieRepository, updateMovieRequest, movieId) =>
            {
                var movie = await new GetMovieUseCase().Execute(movieRepository, movieId);

                if (!movie.MovieId.Equals(movieId))
                    throw new Exception(MovieException.MovieExceptions[MovieExceptionType.MovieNotFound]);

                movie.AlreadySeen = updateMovieRequest.AlreadySeen ?? movie.AlreadySeen;
                movie.IsFavourite = updateMovieRequest.IsFavourite ?? movie.IsFavourite;
                movie.Tag = updateMovieRequest.Tag ?? movie.Tag;
                movie.UserRating = updateMovieRequest.UserRating ?? movie.UserRating;

                var movieTableEntity = await movieRepository.UpdateMovie(movie);

                return movieTableEntity.ToMovie();
            };
    }
}
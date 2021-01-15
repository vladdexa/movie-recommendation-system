using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//TODO change to COSMOS DB SQL CORE 
namespace MovieApi
{
    public class GetRatedMoviesForUserGenreUseCase
    {
        public readonly Func<IMovieRepository, string, Task<IEnumerable<Movie>>> Execute =
            async (movieRepository, userId) =>
            {
                var allMovies = await movieRepository.GetAllMovies();

                var ratedMoviesForUser =
                    allMovies.Where(movie => movie.UserRating != null && movie.UserRating.Contains(userId));

                List<Movie> ratedMoviesForUserDto = new List<Movie>();

                foreach (var movieTableEntity in ratedMoviesForUser)
                    ratedMoviesForUserDto.Add(movieTableEntity.ToMovie());

                return ratedMoviesForUserDto;
            };
    }
}
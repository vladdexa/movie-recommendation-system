using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi
{
    public class GetMoviesByGenreUseCase
    {
        public readonly Func<IMovieRepository, string, Task<IEnumerable<Movie>>> Execute =
            async (movieRepository, genre) =>
            {
                var allMovies = await movieRepository.GetAllMovies();

                var moviesByGenre = allMovies.Where(movie => movie.Genres.Contains(genre));

                var moviesByGenreDto = new List<Movie>();

                foreach (var movieTableEntity in moviesByGenre) moviesByGenreDto.Add(movieTableEntity.ToMovie());

                return moviesByGenreDto;
            };
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi
{
    public class GetMoviesByTagUseCase
    {
        public readonly Func<IMovieRepository, string, Task<IEnumerable<Movie>>> Execute =
            async (movieRepository, tag) =>
            {
                var moviesByTag = await movieRepository.GetMoviesByTag(tag);

                var moviesByTagDto = new List<MovieApi.Movie>();

                foreach (var movieTableEntity in moviesByTag) moviesByTagDto.Add(movieTableEntity.ToMovie());

                return moviesByTagDto;
            };
    }
}
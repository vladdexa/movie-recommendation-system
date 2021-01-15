using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi
{
    public interface IMovieRepository
    {
        public Task<MovieTableEntity> CreateMovie(Movie movie);

        public Task<IEnumerable<MovieTableEntity>> GetMoviesByTag(string tag);

        public Task<IEnumerable<MovieTableEntity>> GetAllMovies();

        public Task<MovieTableEntity> GetMovie(string movieId);
        
        public Task<MovieTableEntity> UpdateMovie(Movie movie);

        public Task DeleteMovie(Movie movie);
    }
}
using System;
using System.Threading.Tasks;

namespace MovieApi
{
    public class CreateMovieUseCase
    {
        public readonly Func<IMovieRepository, CreateMovieRequest, Task<Movie>> Execute =
            async (movieRepository, createMovieRequest) =>
            {
                var movieToCreate = new Movie
                {
                    Name = createMovieRequest.Name,
                    Genres = createMovieRequest.Genres,
                    Rating = createMovieRequest.Rating ?? "",
                    Cast = createMovieRequest.Cast,
                    Directors = createMovieRequest.Directors,
                    Description = createMovieRequest.Description,
                    Duration = createMovieRequest.Duration,
                    ReleaseDate = createMovieRequest.ReleaseDate,
                    PosterImage = createMovieRequest.PosterImage,
                    TrailerLink = createMovieRequest.TrailerLink,
                    MovieDetails = createMovieRequest.MovieDetails,
                    Tag = createMovieRequest.Tag,
                };

                var movieTableEntity = await movieRepository.CreateMovie(movieToCreate);

                return movieTableEntity.ToMovie();
            };
    }
}
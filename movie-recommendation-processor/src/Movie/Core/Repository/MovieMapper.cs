using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieApi
{
    public static class MovieMapper
    {
        public static MovieTableEntity ToMovieTableEntity(this Movie movie)
        {
            return new MovieTableEntity
            {
                PartitionKey = "MOVIE",
                RowKey = movie.MovieId,
                Name = movie.Name,
                Genres = JsonConvert.SerializeObject(movie.Genres, Formatting.Indented),
                Rating = movie.Rating,
                Cast = JsonConvert.SerializeObject(movie.Cast, Formatting.Indented),
                Directors = JsonConvert.SerializeObject(movie.Directors,
                    Formatting.Indented),
                IsFavourite = movie.IsFavourite,
                AlreadySeen = movie.AlreadySeen,
                Description = movie.Description,
                Duration = movie.Duration,
                ReleaseDate = movie.ReleaseDate,
                PosterImage = movie.PosterImage,
                TrailerLink = movie.TrailerLink,
                MovieDetails = JsonConvert.SerializeObject(movie.MovieDetails,
                    Formatting.Indented),
                Tag = movie.Tag,
                UserRating = JsonConvert.SerializeObject(movie.UserRating, Formatting.Indented)
            };
        }

        public static Movie ToMovie(this MovieTableEntity movieTableEntity)
        {
            return new Movie
            {
                MovieId = movieTableEntity.RowKey,
                Name = movieTableEntity.Name,
                Genres = JsonConvert.DeserializeObject<IEnumerable<string>>(movieTableEntity.Genres),
                Rating = movieTableEntity.Rating,
                Cast = JsonConvert.DeserializeObject<IEnumerable<Actor>>(movieTableEntity.Cast),
                Directors =
                    JsonConvert.DeserializeObject<IEnumerable<Actor>>(movieTableEntity.Directors),
                IsFavourite = movieTableEntity.IsFavourite,
                AlreadySeen = movieTableEntity.AlreadySeen,
                Description = movieTableEntity.Description,
                Duration = movieTableEntity.Duration,
                ReleaseDate = movieTableEntity.ReleaseDate,
                PosterImage = movieTableEntity.PosterImage,
                TrailerLink = movieTableEntity.TrailerLink,
                MovieDetails =
                    JsonConvert.DeserializeObject<MovieDetails>(movieTableEntity.MovieDetails ?? ""),
                Tag = movieTableEntity.Tag,
                UserRating = JsonConvert.DeserializeObject<IEnumerable<UserRating>>(movieTableEntity.UserRating ?? "")
            };
        }
    }
}
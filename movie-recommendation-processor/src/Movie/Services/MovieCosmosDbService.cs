using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Table;

namespace MovieApi
{
    public class MovieCosmosDbMovieService : IMovieRepository
    {
        private readonly CloudTable _table;

        public MovieCosmosDbMovieService()
        {
            var connectionString = Environment.GetEnvironmentVariable("COSMOS_DB_DEV_CONNECTION");

            if (string.IsNullOrEmpty(connectionString)) throw new Exception("Please provide credentials");

            var tableName = Environment.GetEnvironmentVariable("MOVIES_TABLE_NAME");

            if (string.IsNullOrEmpty(tableName)) throw new Exception("Please provide movies table name");

            var storageAccount = CloudStorageAccount.Parse(connectionString);

            var tableClient = storageAccount.CreateCloudTableClient();

            var table = tableClient.GetTableReference(tableName);

            _table = table;
        }

        public async Task<MovieTableEntity> CreateMovie(Movie movie)
        {
            try
            {
                return (await _table.ExecuteAsync(TableOperation.Insert(movie.ToMovieTableEntity())))
                    .Result as MovieTableEntity;
            }
            catch (CosmosException error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<IEnumerable<MovieTableEntity>> GetMoviesByTag(string tag)
        {
            try
            {
                return _table.CreateQuery<MovieTableEntity>()
                    .Where(movieTableEntity => movieTableEntity.PartitionKey == "MOVIE" &&
                                               movieTableEntity.Tag == tag).Select(
                        movieTableEntity =>
                            new MovieTableEntity
                            {
                                PartitionKey = movieTableEntity.PartitionKey,
                                RowKey = movieTableEntity.RowKey,
                                Name = movieTableEntity.Name,
                                Genres = movieTableEntity.Genres,
                                Rating = movieTableEntity.Rating,
                                Cast = movieTableEntity.Cast,
                                Directors = movieTableEntity.Directors,
                                IsFavourite = movieTableEntity.IsFavourite,
                                AlreadySeen = movieTableEntity.AlreadySeen,
                                Description = movieTableEntity.Description,
                                Duration = movieTableEntity.Duration,
                                ReleaseDate = movieTableEntity.ReleaseDate,
                                TrailerLink = movieTableEntity.TrailerLink,
                                PosterImage = movieTableEntity.PosterImage,
                                MovieDetails = movieTableEntity.MovieDetails,
                                Tag = movieTableEntity.Tag,
                                UserRating = movieTableEntity.UserRating
                            });
            }
            catch (CosmosException error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<MovieTableEntity> GetMovie(string movieId)
        {
            try
            {
                return (await _table.ExecuteAsync(TableOperation.Retrieve<MovieTableEntity>("MOVIE", movieId))).Result
                    as MovieTableEntity;
            }
            catch (CosmosException error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task<IEnumerable<MovieTableEntity>> GetAllMovies()
        {
            try
            {
                return _table.CreateQuery<MovieTableEntity>()
                    .Where(movieTableEntity => movieTableEntity.PartitionKey == "MOVIE").Select(movieTableEntity =>
                        new MovieTableEntity
                        {
                            PartitionKey = movieTableEntity.PartitionKey,
                            RowKey = movieTableEntity.RowKey,
                            Name = movieTableEntity.Name,
                            Genres = movieTableEntity.Genres,
                            Rating = movieTableEntity.Rating,
                            Cast = movieTableEntity.Cast,
                            Directors = movieTableEntity.Directors,
                            IsFavourite = movieTableEntity.IsFavourite,
                            AlreadySeen = movieTableEntity.AlreadySeen,
                            Description = movieTableEntity.Description,
                            Duration = movieTableEntity.Duration,
                            ReleaseDate = movieTableEntity.ReleaseDate,
                            TrailerLink = movieTableEntity.TrailerLink,
                            PosterImage = movieTableEntity.PosterImage,
                            MovieDetails = movieTableEntity.MovieDetails,
                            Tag = movieTableEntity.Tag,
                            UserRating = movieTableEntity.UserRating
                        });
            }
            catch (CosmosException error)
            {
                throw new Exception(error.Message);
            }
        }


        public async Task<MovieTableEntity> UpdateMovie(Movie movie)
        {
            try
            {
                return (await _table.ExecuteAsync(TableOperation.Replace(movie.ToMovieTableEntity()))).Result
                    as MovieTableEntity;
            }
            catch (CosmosException error)
            {
                throw new Exception(error.Message);
            }
        }

        public async Task DeleteMovie(Movie movie)
        {
            try
            {
                var movieToDelete = movie.ToMovieTableEntity();

                movieToDelete.ETag = "*";

                await _table.ExecuteAsync(TableOperation.Delete(movieToDelete));
            }
            catch (CosmosException error)
            {
                throw new Exception(error.Message);
            }
        }
    }
}
using System.Collections.Generic;

namespace MovieApi
{
    public enum MovieExceptionType
    {
        MovieNameIsRequired,
        MovieRatingIsRequired,
        MovieCastIsRequired,
        MovieDirectorIsRequired,
        MovieNotFound,
        MovieUpdatesBadRequest,
    }

    public enum SearchMovieExceptionType
    {
        SearchMovieRequestFromWrongInput,
        SearchMovieRequestSizeExceeded
    }

    public static class MovieException
    {
        public static readonly Dictionary<MovieExceptionType, string> MovieExceptions =
            new Dictionary<MovieExceptionType, string>
            {
                {MovieExceptionType.MovieNotFound, "Movie not found"},
                {MovieExceptionType.MovieCastIsRequired, "Movie cast is a required field"},
                {MovieExceptionType.MovieDirectorIsRequired, "Movie Directors is a required field"},
                {MovieExceptionType.MovieNameIsRequired, "Movie Name is a required field"},
                {MovieExceptionType.MovieRatingIsRequired, "Movie Rating is a required field"},
                {MovieExceptionType.MovieUpdatesBadRequest, "Movie Updates Bad Request"}
            };

        public static readonly Dictionary<SearchMovieExceptionType, string> SearchMovieExceptions =
            new Dictionary<SearchMovieExceptionType, string>
            {
                {
                    SearchMovieExceptionType.SearchMovieRequestSizeExceeded,
                    $"The request size must be lower than {SearchMovieRequest.DefaultSize}"
                },
                {
                    SearchMovieExceptionType.SearchMovieRequestFromWrongInput,
                    $"The request from must be grater than {SearchMovieRequest.DefaultFrom}"
                }
            };
    }
}
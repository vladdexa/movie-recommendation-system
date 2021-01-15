using System;
using Microsoft.Azure.Documents;

namespace MovieApi
{
    public class SearchMovieRequestPayload
    {
#nullable enable

        public int? From { get; set; }

        public int? Size { get; set; }

        public string? Query { get; set; }
    }

#nullable disable
    public class SearchMovieRequest
    {
        public static readonly int DefaultFrom = 0;

        public static readonly int DefaultSize = 16;

        public static readonly Func<SearchMovieRequestPayload, SearchMovieRequest> Create = (payload) =>
        {
            if (payload.From < DefaultFrom)
            {
                throw new Exception(
                    MovieException.SearchMovieExceptions[SearchMovieExceptionType.SearchMovieRequestFromWrongInput]);
            }

            if (payload.Size > DefaultSize)
            {
                throw new Exception(
                    MovieException.SearchMovieExceptions[SearchMovieExceptionType.SearchMovieRequestSizeExceeded]);
            }

            return new SearchMovieRequest(payload);
        };


        public readonly int From;

        public readonly int Size;

#nullable enable
        public readonly string? Query;
#nullable disable

        private SearchMovieRequest(SearchMovieRequestPayload payload)
        {
            From = payload.From ?? DefaultFrom;
            Size = payload.Size ?? DefaultSize;

            Query = payload.Query;
        }
    }
}
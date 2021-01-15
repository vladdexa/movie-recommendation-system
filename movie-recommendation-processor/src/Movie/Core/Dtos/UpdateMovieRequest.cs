using System.Collections.Generic;

namespace MovieApi
{
    public class UpdateMovieRequest
    {
#nullable enable
        public bool? IsFavourite { get; set; }

        public bool? AlreadySeen { get; set; }

        public string? Tag { get; set; }

        public IEnumerable<UserRating>? UserRating { get; set; }

#nullable disable
    }
}
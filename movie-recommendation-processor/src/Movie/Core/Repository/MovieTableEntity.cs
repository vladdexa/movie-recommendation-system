using Microsoft.Azure.Cosmos.Table;

namespace MovieApi
{
    public class MovieTableEntity : TableEntity
    {
        public string Name { get; set; }

        public string Genres { get; set; }

        public string Rating { get; set; }

        public string Cast { get; set; }

        public string Directors { get; set; }

        public bool IsFavourite { get; set; }

        public bool AlreadySeen { get; set; }
        
#nullable enable
        public string? Description { get; set; }

        public string? Duration { get; set; }

        public string? ReleaseDate { get; set; }

        public string? TrailerLink { get; set; }

        public string? PosterImage { get; set; }

        public string? MovieDetails { get; set; }

        public string? Tag { get; set; }
        
        public string? UserRating { get; set; }

#nullable disable
    }
}
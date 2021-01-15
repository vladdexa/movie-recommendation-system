using System;
using System.Collections.Generic;

namespace MovieApi
{
    public class Movie
    {
        public string MovieId { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public string Rating { get; set; }
        
        public IEnumerable<Actor> Cast { get; set; }

        public IEnumerable<Actor> Directors { get; set; }
        
        public bool IsFavourite { get; set; }

        public bool AlreadySeen { get; set; }

#nullable enable
        public string? Description { get; set; }

        public string? Duration { get; set; }

        public string? ReleaseDate { get; set; }

        public string? TrailerLink { get; set; }

        public string? PosterImage { get; set; }

        public string? Tag { get; set; }

        public MovieDetails? MovieDetails { get; set; }
        
        public IEnumerable<UserRating>? UserRating { get; set; }
        
#nullable disable
    }
}
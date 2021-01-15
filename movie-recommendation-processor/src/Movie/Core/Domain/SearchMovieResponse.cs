using System.Collections.Generic;

namespace MovieApi
{
    public class SearchMovieResponse
    {
        public IEnumerable<Movie> Movies;

        public int Total;
    }
}
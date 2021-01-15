using System.Collections.Generic;
using FluentValidation;

namespace MovieApi
{
    public class CreateMovieRequestValidator : AbstractValidator<CreateMovieRequest>
    {
        public CreateMovieRequestValidator()
        {
            RuleFor(createMovieRequest => createMovieRequest.Name).NotNull().NotEmpty()
                .WithMessage(MovieException.MovieExceptions[MovieExceptionType.MovieNameIsRequired]);

            RuleFor(createMovieRequest => createMovieRequest.Rating).NotNull().NotEmpty()
                .WithMessage(MovieException.MovieExceptions[MovieExceptionType.MovieRatingIsRequired]);

            RuleFor(createMovieRequest => createMovieRequest.Cast).NotNull().NotEmpty()
                .WithMessage(MovieException.MovieExceptions[MovieExceptionType.MovieCastIsRequired]);

            RuleFor(createMovieRequest => createMovieRequest.Directors).NotNull().NotEmpty()
                .WithMessage(MovieException.MovieExceptions[MovieExceptionType.MovieDirectorIsRequired]);
        }
    }

    public class CreateMovieRequest
    {
#nullable enable
        public string? Name { get; set; }

        public IEnumerable<string>? Genres { get; set; }

        public string? Rating { get; set; }

        public IEnumerable<Actor>? Cast { get; set; }

        public IEnumerable<Actor>? Directors { get; set; }

        public string? Description { get; set; }

        public string? Duration { get; set; }

        public string? ReleaseDate { get; set; }

        public string? TrailerLink { get; set; }

        public string? PosterImage { get; set; }

        public string? Tag { get; set; }
        public MovieDetails? MovieDetails { get; set; }
#nullable disable
    }
}
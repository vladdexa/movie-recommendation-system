using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Movie.Config;
using MovieApi;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Movie.Config
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging(loggingBuilder => { loggingBuilder.AddFilter(level => true); });

            builder.Services.AddSingleton<IMovieRepository, MovieCosmosDbMovieService>();
        }
    }
}
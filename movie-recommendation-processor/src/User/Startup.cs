using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using User.Config;
using UserApi;

[assembly: FunctionsStartup(typeof(Startup))]

namespace User.Config
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging(loggingBuilder => { loggingBuilder.AddFilter(level => true); });

            builder.Services.AddSingleton<IUserRepository, UserCosmosDbService>();
        }
    }
}
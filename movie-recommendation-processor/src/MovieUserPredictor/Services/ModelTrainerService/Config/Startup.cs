using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using MovieUserPredictor.Services.AzureStorageService;
using MovieUserPredictor.Services.ModelTrainerService.Config;

[assembly: WebJobsStartup(typeof(Startup))]

namespace MovieUserPredictor.Services.ModelTrainerService.Config
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddLogging(logginBuilder => { logginBuilder.AddFilter(level => true); });

            builder.Services.AddSingleton(sp => new MLContext(0));
            builder.Services.AddSingleton<IAzureStorageService, AzureStorageService.AzureStorageService>();
            builder.Services.AddSingleton<IModelTrainerService, ModelTrainerService>();
        }
    }
}
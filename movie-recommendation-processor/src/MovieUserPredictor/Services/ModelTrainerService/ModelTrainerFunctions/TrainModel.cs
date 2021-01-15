using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using MovieUserPredictor.Services.AzureStorageService;

namespace MovieUserPredictor.Services.ModelTrainerService.ModelTrainerFunctions
{
    public class TrainModel
    {
        private readonly IAzureStorageService _azureStorageService;
        private readonly MLContext _mlContext;
        private readonly IModelTrainerService _modelTrainerService;

        public TrainModel(
            MLContext mlContext,
            IAzureStorageService azureStorageService, IModelTrainerService modelTrainerService)
        {
            _mlContext = mlContext;
            _azureStorageService = azureStorageService;
            _modelTrainerService = modelTrainerService;
        }

        [FunctionName(nameof(TrainModel))]
        public async Task Run([TimerTrigger("5 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");

            try
            {
                var trainDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "movie-prediction-train.csv");
                var testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "movie-prediction-test.csv");
                var modelPath = Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_CONTAINER_NAME");

                if (string.IsNullOrEmpty(modelPath)) throw new Exception("Please provide Model Path");

                log.LogInformation("Training the model");

                var model = _modelTrainerService.TrainModel(_mlContext, trainDataPath);

                log.LogInformation("Evaluating the model");

                var evaluationResult = _modelTrainerService.EvaluateModel(_mlContext, model, testDataPath);

                var dataView =
                    _mlContext.Data.LoadFromTextFile<MovieRating>(trainDataPath, hasHeader: true, separatorChar: ',');

                if (evaluationResult >= 0.7)
                {
                    log.LogInformation("Good fit! Saving model");
                    await _modelTrainerService.SaveModel(_mlContext, _azureStorageService, model, dataView, modelPath);
                }
                else
                {
                    log.LogInformation("The model is poor fit");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
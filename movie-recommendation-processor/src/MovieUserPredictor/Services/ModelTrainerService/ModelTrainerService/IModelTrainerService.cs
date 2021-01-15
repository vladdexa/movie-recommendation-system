using System.Threading.Tasks;
using Microsoft.ML;
using MovieUserPredictor.Services.AzureStorageService;

namespace MovieUserPredictor.Services.ModelTrainerService
{
    public interface IModelTrainerService
    {
        public ITransformer TrainModel(MLContext mlContext, string trainFilePath);

        public Task SaveModel(MLContext mlContext, IAzureStorageService storageService, ITransformer model,
            IDataView dataView, string modelPath);

        public double EvaluateModel(MLContext mlContext, ITransformer model, string testFilePath);
    }
}
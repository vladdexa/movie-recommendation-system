using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using MovieUserPredictor.Services.AzureStorageService;

namespace MovieUserPredictor.Services.ModelTrainerService
{
    public class ModelTrainerService : IModelTrainerService
    {
        public ITransformer TrainModel(MLContext mlContext, string trainFilePath)
        {
            var trainingDataView =
                mlContext.Data.LoadFromTextFile<MovieRating>(trainFilePath, hasHeader: true, separatorChar: ',');

            var options = new MatrixFactorizationTrainer.Options();
            options.MatrixColumnIndexColumnName = "userIdEncoded";
            options.MatrixRowIndexColumnName = "movieIdEncoded";
            options.LabelColumnName = "Label";
            options.NumberOfIterations = 20;
            options.ApproximationRank = 100;

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey(
                    inputColumnName: "userId",
                    outputColumnName: "userIdEncoded")
                .Append(mlContext.Transforms.Conversion.MapValueToKey(
                        inputColumnName: "movieId",
                        outputColumnName: "movieIdEncoded")
                    .Append(mlContext.Recommendation().Trainers.MatrixFactorization(options)));

            var model = pipeline.Fit(trainingDataView);

            return model;
        }

        public async Task SaveModel(MLContext mlContext, IAzureStorageService storageService, ITransformer model,
            IDataView dataView, string modelPath)
        {
            mlContext.Model.Save(model, dataView.Schema, modelPath);

            await storageService.UploadBlobToStorage(modelPath);
        }

        public double EvaluateModel(MLContext mlContext, ITransformer model, string testFilePath)
        {
            var dataView =
                mlContext.Data.LoadFromTextFile<MovieRating>(testFilePath, hasHeader: true, separatorChar: ',');

            var predictions = model.Transform(dataView);

            var metrics = mlContext.Regression.Evaluate(predictions);

            var rSquaredValue = metrics.RSquared;

            return rSquaredValue;
        }
    }
}
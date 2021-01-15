using Microsoft.ML.Data;

namespace MovieUserPredictor.Services.ModelTrainerService
{
    public class MovieRating
    {
        [LoadColumn(0)] public string userId;
        [LoadColumn(1)] public string movieId;
        [LoadColumn(2)] public float Label;
    }
}
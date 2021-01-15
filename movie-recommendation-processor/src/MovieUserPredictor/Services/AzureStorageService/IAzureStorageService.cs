using System.Threading.Tasks;

namespace MovieUserPredictor.Services.AzureStorageService
{
    public interface IAzureStorageService
    {
        Task UploadBlobToStorage(string blobName);
        Task DownloadBlob(string blobName);
    }
}
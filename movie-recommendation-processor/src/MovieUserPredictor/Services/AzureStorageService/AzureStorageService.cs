using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace MovieUserPredictor.Services.AzureStorageService
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly CloudBlobContainer _container;

        public AzureStorageService()
        {
            var storageAccountConnectionString =
                Environment.GetEnvironmentVariable("STORAGE_ACCOUNT_CONNECTION_STRING");
            if (string.IsNullOrEmpty(storageAccountConnectionString))
                throw new Exception("Please provide Storage Account ConnectionString");

            var storageAccountContainerName = Environment.GetEnvironmentVariable("STORAGE_CONTAINER_NAME");
            if (string.IsNullOrEmpty(storageAccountContainerName))
                throw new Exception("Please provide Storage Account Container Name");

            try
            {
                var storageAccount = CloudStorageAccount.Parse(storageAccountConnectionString);
                var blobClient = storageAccount.CreateCloudBlobClient();

                _container = blobClient.GetContainerReference(storageAccountContainerName);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UploadBlobToStorage(string blobName)
        {
            var cloudBlockBlob = _container.GetBlockBlobReference(blobName);
            await cloudBlockBlob.UploadFromFileAsync(blobName);
        }

        public async Task DownloadBlob(string blobName)
        {
            var cloudBlockBlob = _container.GetBlockBlobReference(blobName);
            await cloudBlockBlob.DownloadToFileAsync(blobName, FileMode.Open);
        }
    }
}
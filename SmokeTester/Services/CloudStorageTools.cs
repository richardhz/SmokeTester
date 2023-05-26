using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;

namespace SmokeTester.Services
{
    public class CloudStorageTools : ICloudStorageTools
    {
        private BlockBlobClient ConnectToAzureStorage(string fileName)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=rhzcloudstorage;AccountKey=x0hCzR7JAJJ+hbEqEe2gyaMqMmRqiPMs2KAtCF4CCnqA3RTYHmGQuZR/+a0/RZEKbefkD+meOow7+AStbPoyJw==;EndpointSuffix=core.windows.net";
            string containerName = "testtooldata";
            string blobName = $"Team-{fileName}";

            var bsc = new BlobServiceClient(connectionString);

            var cc = bsc.GetBlobContainerClient(containerName);
            if (!cc.Exists())
            {
                cc = bsc.CreateBlobContainer(containerName);
            }
            return cc.GetBlockBlobClient(blobName);
        }

        public async Task<string> DownloadProfleAsync(string filename)
        {
            using var ms = new MemoryStream();
            string jsonData = string.Empty;
            var bbc = ConnectToAzureStorage(filename);
            if (bbc.Exists())
            {
                await bbc.DownloadToAsync(ms);
                jsonData = System.Text.Encoding.UTF8.GetString(ms.ToArray());
            }
            return jsonData;
            
        }

        public async Task UploadProfileAsync(string jsonData, string filename)
        {
            byte[] data = Encoding.UTF8.GetBytes(jsonData);

            using MemoryStream stream = new MemoryStream(data);
            var bbc = ConnectToAzureStorage(filename);
            await bbc.UploadAsync(stream);
        }
    }
}

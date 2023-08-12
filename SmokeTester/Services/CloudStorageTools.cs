using Azure.Storage.Blobs.Specialized;
using System.Text;

namespace SmokeTester.Services
{
    public class CloudStorageTools : ICloudStorageTools
    {
        private readonly BlobServiceClient _bsc;
        public CloudStorageTools(BlobServiceClient blobServiceClient)
        {
            _bsc = blobServiceClient;
        }

        private BlockBlobClient ConnectToAzureStorage(string fileName)
        {
            string containerName = "testtooldata";
            string blobName = $"Team-{fileName}";

            try
            {
                var cc = _bsc.GetBlobContainerClient(containerName);
                if (!cc.Exists())
                {
                    cc = _bsc.CreateBlobContainer(containerName);
                }
                return cc.GetBlockBlobClient(blobName);
            }
            catch (Exception ex)
            {
                //display status message
                var em = ex.Message;
                throw;
            }
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

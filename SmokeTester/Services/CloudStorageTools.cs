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
        public IPublicClientApplication IdentityClient { get; set; }

        private async Task<BlockBlobClient> ConnectToAzureStorage(string fileName)
        {
            var clientId = "77373c79-5e06-48b8-a974-b91eb2b9cdc0";
            var tenantId = "8fda8d81-ebbf-4c25-9b08-2453e217680f";
            string scope = ".default";
            var fullscope = $"{clientId}/{scope}";
            IdentityClient = PublicClientApplicationBuilder.Create(clientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, tenantId)
                .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient")
                .Build();

            var accounts = await IdentityClient.GetAccountsAsync();
            var firstAccount = accounts.FirstOrDefault();
            var authResult = await IdentityClient.AcquireTokenSilent(new[] { fullscope }, firstAccount)
                                                  .ExecuteAsync();

            //string connectionString = "";
            string containerName = "testtooldata";
            string blobName = $"Team-{fileName}";

            //var bsc = new BlobServiceClient(connectionString);

            try
            {
                var bsc = new BlobServiceClient(new Uri("https://sbrholding.blob.core.windows.net"), new DefaultAzureCredential());
                var cc = bsc.GetBlobContainerClient(containerName);
                if (!cc.Exists())
                {
                    cc = bsc.CreateBlobContainer(containerName);
                }
                return cc.GetBlockBlobClient(blobName);
            }
            catch (Exception ex)
            {
                var em = ex.Message;
                throw;
            }
        }

        public async Task<string> DownloadProfleAsync(string filename)
        {
            using var ms = new MemoryStream();
            string jsonData = string.Empty;
            var bbc = await ConnectToAzureStorage(filename);
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
            var bbc = await ConnectToAzureStorage(filename);
            await bbc.UploadAsync(stream);
        }
    }
}

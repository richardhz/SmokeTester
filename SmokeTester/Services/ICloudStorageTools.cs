using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeTester.Services
{
    public interface ICloudStorageTools
    {
        Task UploadProfileAsync(string jsonData, string filename);
        Task<string> DownloadProfleAsync(string filename);
    }
}

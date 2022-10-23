using System.Collections.Generic;
using System.Threading.Tasks;
using MultiClouding.Interfaces;
using MultiClouding.Models;

namespace MultiClouding.Services;

public class GoogleDriveService : ICloudService
{
    public async Task Authenticate()
    {
        throw new System.NotImplementedException();
    }

    public async Task<List<CloudFile>> GetFiles()
    {
        throw new System.NotImplementedException();
    }

    public async Task DownloadFile(CloudFile file)
    {
        throw new System.NotImplementedException();
    }

    public async Task UploadFile(CloudFile file)
    {
        throw new System.NotImplementedException();
    }

    public async Task RenameFile(CloudFile file)
    {
        throw new System.NotImplementedException();
    }
}
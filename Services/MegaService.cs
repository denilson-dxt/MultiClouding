using System.Collections.Generic;
using System.Threading.Tasks;
using CG.Web.MegaApiClient;
using MultiClouding.Interfaces;
using MultiClouding.Models;

namespace MultiClouding.Services;

public class MegaService : ICloudService
{
    public string GetName() => "Mega";
    public string GetIcon() => "mega.png";

    public async Task<ICloudService> Authenticate(object? authenticationArgs = null)
    {
        MegaApiClient client = authenticationArgs as MegaApiClient;
        var info = await client.GetAccountInformationAsync();
        return this;
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
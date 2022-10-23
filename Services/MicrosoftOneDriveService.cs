using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.Authentication;
using MultiClouding.Interfaces;
using MultiClouding.Models;

namespace MultiClouding.Services;

public class MicrosoftOneDriveService : ICloudService
{
    public string GetName() => "Microsoft OneDrive";

    public async Task<ICloudService> Authenticate()
    {
        var auth = new MsaAuthenticationProvider("a1caf8f9-f772-4138-9546-7145a34d2d05", "https://login.live.com/oauth20_desktop.srf", new []{"onedrive.readonly", "wl.signin"});
        await auth.AuthenticateUserAsync();
        var client = new OneDriveClient(auth);
        var t = await client.Drive.Root.Request().GetAsync();
        return new MicrosoftOneDriveService();
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
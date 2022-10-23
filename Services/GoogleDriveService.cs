using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using MultiClouding.Interfaces;
using MultiClouding.Models;

namespace MultiClouding.Services;

public class GoogleDriveService : ICloudService
{
    private DriveService _service;
    public string GetName() => "Google drive";
   

    public async Task<ICloudService> Authenticate()
    {
        UserCredential credential;
        using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.ReadWrite))
        {
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets,
                new[] {"https://www.googleapis.com/auth/drive.readonly"}, "user", CancellationToken.None);
        }

        var service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "Multi-clouding"
        });
        
        return new GoogleDriveService(){_service = service};
    }

    public async Task<List<CloudFile>> GetFiles()
    {
        var files = new List<CloudFile>();
        var dFiles = _service.Files.List().Execute();
        foreach (var file in dFiles.Items)
        {
            files.Add(new CloudFile(){Name = file.OriginalFilename});
        }

        return files;
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using MultiClouding.Enums;
using MultiClouding.Interfaces;
using MultiClouding.Models;

namespace MultiClouding.Services;

public class GoogleDriveService : ICloudService
{
    private DriveService _service;
    public string GetName() => "Google drive";
    public string GetIcon() => "google-drive.png";


    public async Task<ICloudService> Authenticate(object? authenticationArgs = null)
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
        var request = _service.Files.List();
        request.Q = "'root' in parents";
        request.OrderBy = "folder";
        var dFiles = request.Execute();
        foreach (var file in dFiles.Items)
        {
            files.Add(new CloudFile()
            {
                Name = file.Title,
                Link = file.AlternateLink,
                ModifiedAt = (DateTime) file.ModifiedDate,
                Type = file.AlternateLink.Contains("folders") ? CloudFileType.Folder : CloudFileType.File
            });
        }
        return files;
    }

    public async Task<UserInfo> GetUserInfo()
    {
        var about = await _service.About.Get().ExecuteAsync();
        var imageStream = await _getProfilePictureStreamFromUrl(about.User.Picture.Url);
        
        return new UserInfo()
        {
            Username = about.User.DisplayName,
            Email = about.User.EmailAddress,
            ProfilePictureStream = imageStream
        };
    }

    private static async Task<Stream> _getProfilePictureStreamFromUrl(string url)
    {
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        return await res.Content.ReadAsStreamAsync();
        //return res;
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
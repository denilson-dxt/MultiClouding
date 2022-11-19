using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using MultiClouding.Enums;
using MultiClouding.Interfaces;
using MultiClouding.Models;

namespace MultiClouding.Services;

public class MicrosoftOneDriveService : ICloudService
{
    private GraphServiceClient _graphServiceClient = null;
    private AccessToken _accessToken;
    public string GetName() => "Microsoft OneDrive";
    public string GetIcon() => "onedrive.png";

    public ICloudService Authenticate(GraphServiceClient graphServiceClient, AccessToken accessToken)
    {
        _graphServiceClient = graphServiceClient;
        _accessToken = accessToken;
        //await _authorize();
        return this;
    }
    private async Task _authorize()
    {
        string[] scopes = {"User.Read"};
        var clientId = "a1caf8f9-f772-4138-9546-7145a34d2d05";
        var tenantId = "f8cdef31-a31e-4b4a-93e4-5f571e91255a";
        var authTenant = "consumers";
        var deviceAuth = new DeviceCodeCredential((info, cancel) =>
        {
            Console.WriteLine(info.Message);
            return Task.FromResult(0);
        }, authTenant, clientId);
        
       _graphServiceClient = new GraphServiceClient(deviceAuth, scopes); // you can pass the TokenCredential directly to the GraphServiceClient
       var token  = await deviceAuth.GetTokenAsync(new TokenRequestContext(scopes));
       
    }

    public async Task<List<CloudFile>> GetFiles(string parentId="")
    {
        var files = new List<CloudFile>();
        var requestHeaders = new List<HeaderOption>()
        {
            new HeaderOption("Authorization", $"Bearer {_accessToken.Token}")
        };
        var oDFiles = await _graphServiceClient.Me.Drive.Root.Children.Request(requestHeaders).GetAsync();
        foreach (var file in oDFiles)
        {
            files.Add(new CloudFile()
            {
                Name = file.Name,
                Id = file.Id,
                Type = file.Folder == null ? CloudFileType.File : CloudFileType.Folder,
                ModifiedAt = file.LastModifiedDateTime.Value.DateTime
            });
        }
        return files;
    }

    public async Task<UserInfo> GetUserInfo()
    {
        var users = await _graphServiceClient.Users.Request().GetAsync();
        var user = users.First();
        var pr = await _graphServiceClient.Me.Photo.Content.Request().GetAsync();
        
        return new UserInfo()
        {
            Username = user.DisplayName,
            Email = user.UserPrincipalName,
            ProfilePictureStream = pr
        };
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
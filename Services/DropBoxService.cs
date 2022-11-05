using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Users;
using MultiClouding.Configs;
using MultiClouding.Enums;
using MultiClouding.Interfaces;
using MultiClouding.Models;

namespace MultiClouding.Services;

public class DropBoxService : ICloudService
{
    private string _apiKey = "hsrpha89ddkirvr";
    private string _apiSecret = "fvja6wfasr28fr1";
    private string _host = "http://127.0.0.1:52475/";
    private Uri _redirectUrl = new Uri("http://127.0.0.1:52475/authorize");
    private Uri _jsRedirect = new Uri("http://127.0.0.1:52475/token");

    private DropboxClient _client;
    
    public string GetName() => "DropBox";
    public string GetIcon() => "dropbox.png";

    public async Task<ICloudService> Authenticate(object? authenticationArgs = null)
    {
        var settings = DropBoxSettings.GetStored();
        if (settings != null)
        {
            
            _client = new DropboxClient(settings.AccessToken, settings.RefreshToken, settings.AppKey, settings.AppSecret,
                new DropboxClientConfig());
            FullAccount user = null;
            try
            {
                user = await _client.Users.GetCurrentAccountAsync();
                return this;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        var state = Guid.NewGuid().ToString("N");
        var scopeList = new[] {""};
        IncludeGrantedScopes includedGrantedScopes = IncludeGrantedScopes.User;
        var authUri = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Code, _apiKey, _redirectUrl, state:state, tokenAccessType:TokenAccessType.Offline, scopeList:scopeList, includeGrantedScopes:includedGrantedScopes);
        var http = new HttpListener();
        http.Prefixes.Add(_redirectUrl.OriginalString.Replace("authorize", ""));
        http.Start();
        System.Diagnostics.Process.Start("xdg-open", authUri.ToString());
        await _handleOAuth2Redirect(http);

        var redUri = await HandleJSRedirect(http);
        var tokenResult = await DropboxOAuth2Helper.ProcessCodeFlowAsync(redUri, _apiKey, _apiSecret, _redirectUrl.ToString(), state);
        http.Stop();
        _client = new DropboxClient(tokenResult.AccessToken, tokenResult.RefreshToken, _apiKey, _apiSecret,
            new DropboxClientConfig());
        var newSettings = new DropBoxSettings(_apiKey, _apiSecret, tokenResult.AccessToken, tokenResult.RefreshToken);
        newSettings.Store();
        return this;
    }
    private async Task _handleOAuth2Redirect(HttpListener http)
    {
        var context = await http.GetContextAsync();

        // We only care about request to RedirectUri endpoint.
        while (context.Request.Url.AbsolutePath != _redirectUrl.AbsolutePath)
        {
            context = await http.GetContextAsync();
        }

        context.Response.ContentType = "text/html";

        // Respond with a page which runs JS and sends URL fragment as query string
        // to TokenRedirectUri.
        using (var file = File.OpenRead("index.html"))
        {
            file.CopyTo(context.Response.OutputStream);
        }

        context.Response.OutputStream.Close();
    }
    private async Task<Uri> HandleJSRedirect(HttpListener http)
    {
        var context = await http.GetContextAsync();

        // We only care about request to TokenRedirectUri endpoint.
        while (context.Request.Url.AbsolutePath != _jsRedirect.AbsolutePath)
        {
            context = await http.GetContextAsync();
        }

        var redirectUri = new Uri(context.Request.QueryString["url_with_fragment"]);
            
        return redirectUri;
    }
    
    
    public async Task<List<CloudFile>> GetFiles()
    {
        var files = new List<CloudFile>();
        var dFiles = await _client.Files.ListFolderAsync(string.Empty);
        foreach (var file in dFiles.Entries)
        {
            var cloudFile = new CloudFile()
            {
                Name = file.Name,
                Type = file.IsFile ? CloudFileType.File : CloudFileType.Folder
            };
            if (file.IsFile)
            {
                cloudFile.Id = file.AsFile.Id;
                cloudFile.ModifiedAt = file.AsFile.ServerModified;
                cloudFile.Type = CloudFileType.File;
            }
            else
            {
                cloudFile.Id = file.AsFolder.Id;
                cloudFile.ModifiedAt = DateTime.Now;
                cloudFile.Type = CloudFileType.Folder;
            }
            files.Add(cloudFile);
        }
        return files;
    }

    public async Task<UserInfo> GetUserInfo()
    {
        var user = await _client.Users.GetCurrentAccountAsync();
        var imageStream = await _getProfilePictureStreamFromUrl(user.ProfilePhotoUrl);
        return new UserInfo()
        {
            Username = user.Name.DisplayName,
            Email = user.Email,
            ProfilePictureStream = imageStream
        };
    }

    private static async Task<Stream> _getProfilePictureStreamFromUrl(string url)
    {
        var client = new HttpClient();
        var res = await client.GetStreamAsync(url);
        return res;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CG.Web.MegaApiClient;
using MultiClouding.Enums;
using MultiClouding.Interfaces;
using MultiClouding.Models;

namespace MultiClouding.Services;

public class MegaService : ICloudService
{
    private MegaApiClient _client;
    private string _root;
    public string GetName() => "Mega";
    public string GetIcon() => "mega.png";

    public async Task<ICloudService> Authenticate(object? authenticationArgs = null)
    {
        _client = authenticationArgs as MegaApiClient;
        var info = await _client.GetAccountInformationAsync();
        _root = info.Metrics.First().NodeId;
        return this;
    }

    public async Task<List<CloudFile>> GetFiles(string parentId="")
    {
        var nodes = await _client.GetNodesAsync();
        var files = new List<CloudFile>();
        foreach (var node in nodes)
        {
            if (node.ParentId == _root)
            {
                files.Add(new CloudFile()
                {
                    Name = node.Name,
                    Id = node.Id,
                    ModifiedAt = node.ModificationDate?.Date ?? DateTime.Now,
                    Type = node.Type == NodeType.Directory ? CloudFileType.Folder : CloudFileType.File
                });
            }
        }

        return files;
    }

    public async Task<UserInfo> GetUserInfo()
    {
        return new UserInfo();
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
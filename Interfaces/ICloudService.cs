using System.Collections.Generic;
using System.Threading.Tasks;
using MultiClouding.Models;

namespace MultiClouding.Interfaces;

public interface ICloudService
{
    public string GetName();
    public string GetIcon();
    public Task<List<CloudFile>> GetFiles(string parentId="");
    public Task<UserInfo> GetUserInfo();
    public Task DownloadFile(CloudFile file);
    public Task UploadFile(CloudFile file);
    public Task RenameFile(CloudFile file);
}
using System.Collections.Generic;
using System.Threading.Tasks;
using MultiClouding.Models;

namespace MultiClouding.Interfaces;

public interface ICloudService
{
    public string GetName();
    public string GetIcon();
    public  Task<ICloudService> Authenticate(object? authenticationArgs = null);
    public Task<List<CloudFile>> GetFiles();
    public Task DownloadFile(CloudFile file);
    public Task UploadFile(CloudFile file);
    public Task RenameFile(CloudFile file);
}
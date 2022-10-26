using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MultiClouding.Configs;

public class DropBoxSettings
{
    public string AppKey { get; set; }
    public string AppSecret { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    public DropBoxSettings(string appKey, string appSecret, string accessToken, string refreshToken)
    {
        AppKey = appKey;
        AppSecret = appSecret;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public static DropBoxSettings GetStored()
    {
        if (!File.Exists("dropboxsettings.json"))  return null;

        var settings = JsonSerializer.Deserialize<DropBoxSettings>(File.ReadAllText("dropboxsettings.json"));
        return settings;
    }

    public void Store()
    {
        var jsonContent = JsonSerializer.Serialize(this);
        File.WriteAllText("dropboxsettings.json", jsonContent);
    }
}
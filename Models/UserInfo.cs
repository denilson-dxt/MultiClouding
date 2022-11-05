using System.IO;

namespace MultiClouding.Models;

public class UserInfo
{
    public string Username { get; set; }
    public string Email { get; set; }
    public Stream ProfilePictureStream { get; set; }
}
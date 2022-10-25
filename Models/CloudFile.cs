using System;
using MultiClouding.Enums;

namespace MultiClouding.Models;

public class CloudFile
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
    public CloudFileType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}
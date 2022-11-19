using System;
using MultiClouding.Enums;
using MultiClouding.Models;
using ReactiveUI;

namespace MultiClouding.ViewModels;

public class FileViewModel : ViewModelBase
{
    private string _id;

    public string Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }
    private string _name;
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    private string _size;
    public string Size
    {
        get => _size;
        set => this.RaiseAndSetIfChanged(ref _size, value);
    }

    private CloudFileType _type;

    public CloudFileType Type
    {
        get => _type;
        set => this.RaiseAndSetIfChanged(ref _type, value);
    }
    
    private DateTime _lastModified;
    public DateTime LastModified
    {
        get => _lastModified;
        set => this.RaiseAndSetIfChanged(ref _lastModified, value);
    }

    public FileViewModel(CloudFile file)
    {
        Id = file.Id;
        Name = file.Name;
        Type = file.Type;
        LastModified = file.ModifiedAt;
    }
    
}
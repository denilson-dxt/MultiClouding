using System;
using ReactiveUI;

namespace MultiClouding.ViewModels;

public class FileViewModel : ViewModelBase
{
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

    private DateTime _lastModified;
    public DateTime LastModified
    {
        get => _lastModified;
        set => this.RaiseAndSetIfChanged(ref _lastModified, value);
    }
    
}
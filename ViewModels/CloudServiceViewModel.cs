using MultiClouding.Interfaces;
using ReactiveUI;

namespace MultiClouding.ViewModels;

public class CloudServiceViewModel: ViewModelBase
{
    private ICloudService _service;
    public ICloudService Service
    {
        get => _service;
        set => this.RaiseAndSetIfChanged(ref _service, value);
    }

    private string _name;
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    private string _icon;
    public string Icon
    {
        get => _icon;
        set => this.RaiseAndSetIfChanged(ref _icon, value);
    }
    
    public CloudServiceViewModel(ICloudService service)
    {
        Service = service;
        Name = service.GetName();
    }
}
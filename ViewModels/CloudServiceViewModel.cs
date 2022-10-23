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
    
    public CloudServiceViewModel(ICloudService service)
    {
        Service = service;
        Name = service.GetName();
    }
}
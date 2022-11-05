using System.Threading.Tasks;
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

    private UserInfoViewModel _userInfo;

    public UserInfoViewModel UserInfo
    {
        get => _userInfo;
        set => this.RaiseAndSetIfChanged(ref _userInfo, value);
    }
    
    public CloudServiceViewModel(ICloudService service)
    {
        Service = service;
        Name = service.GetName();
        Icon = service.GetIcon();
        Task.Run(async () =>
        {
            await _getUserInfo();
        });
    }

    private async Task _getUserInfo()
    {
        var userInfo = await Service.GetUserInfo();
        UserInfo = new UserInfoViewModel()
        {
            Username = userInfo.Username,
            Email = userInfo.Email,
            ProfilePictureStream = userInfo.ProfilePictureStream
        };
    }
}
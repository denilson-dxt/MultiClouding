using System.Reactive;
using CG.Web.MegaApiClient;
using MultiClouding.Interfaces;
using MultiClouding.Services;
using ReactiveUI;

namespace MultiClouding.ViewModels;

public class LoginMegaAccountViewModel : ViewModelBase
{
    private string _email;

    public string Email
    {
        get => _email;
        set => this.RaiseAndSetIfChanged(ref _email, value);
    }
    private string _password;

    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    public ICloudService Service;
    public LoginMegaAccountViewModel()
    {
        CloseCommand = ReactiveCommand.Create(() =>
        {
            return Service;
        });
        LoginCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            MegaApiClient client = new MegaApiClient();
            client.Login(Email, Password);
            Service = await new MegaService().Authenticate(client);
            return Service;
        });
    }
    
    public ReactiveCommand<Unit, ICloudService> LoginCommand { get; set; }
    public ReactiveCommand<Unit, ICloudService> CloseCommand { get; set; }
}
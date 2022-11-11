using System.Reactive;
using System.Threading.Tasks;
using CG.Web.MegaApiClient;
using MultiClouding.Interfaces;
using MultiClouding.Services;
using MultiClouding.Views;
using ReactiveUI;

namespace MultiClouding.ViewModels;

public class LoginMegaAccountViewModel : ServiceRegisterViewModelBase
{
    private string _email;
    public override object WindowType { get; set; } = typeof(LoginMegaAccount);
    public override string ServiceName { get; set; } = "Mega";

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

    // public ICloudService Service;
    public override ICloudService Service { get; set; }

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
            //CloseCommand.Execute();
            //return Service;
        });
    }

    public ReactiveCommand<Unit, Unit> LoginCommand { get; set; }
    public ReactiveCommand<Unit, ICloudService> CloseCommand { get; set; }
    
}
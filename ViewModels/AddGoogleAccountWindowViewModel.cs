using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Threading;
using MultiClouding.Interfaces;
using MultiClouding.Services;
using MultiClouding.Views;
using ReactiveUI;

namespace MultiClouding.ViewModels;

public class AddGoogleAccountWindowViewModel : ServiceRegisterViewModelBase
{
    public override object WindowType { get; set; } = typeof(AddGoogleDriveAccountWindow);

    private string _closeBtnText = "Do not close yet";
    public string CloseBtnText
    {
        get => _closeBtnText;
        set => this.RaiseAndSetIfChanged(ref _closeBtnText, value);
    }

    private bool _hasFinished;
    public bool HasFinished
    {
        get => _hasFinished;
        set => this.RaiseAndSetIfChanged(ref _hasFinished, value);
    }
    
    // public ICloudService Service { get; set; }
    public override ICloudService Service { get; set; }
    public override string ServiceName { get; set; } = "Google drive";

    public AddGoogleAccountWindowViewModel()
    {
        
        CloseCommand = ReactiveCommand.Create(() =>
        {
            return Service;
        });
        
        LoginCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await _authenticate();
            HasFinished = true;
        });
    }


    private async Task _authenticate()
    {
        Service = await new GoogleDriveService().Authenticate();
    }
    public ReactiveCommand<Unit, ICloudService> CloseCommand { get; set; }
    public ReactiveCommand<Unit, Unit> LoginCommand { get; set; }
}
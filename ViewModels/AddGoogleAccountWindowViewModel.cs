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
    
    // public ICloudService Service { get; set; }
    public override ICloudService Service { get; set; }
    public override string ServiceName { get; set; } = "Google drive";

    public AddGoogleAccountWindowViewModel()
    {
        
        CloseCommand = ReactiveCommand.Create(() =>
        {
            return Service;
        });
        Task.Run(async () =>
        {
            await _authenticate();
            CloseBtnText = "Close";
        });
    }


    private async Task _authenticate()
    {
        Service = await new GoogleDriveService().Authenticate();
    }
    public ReactiveCommand<Unit, ICloudService> CloseCommand { get; set; }
}
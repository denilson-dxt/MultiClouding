using System.Reactive;
using System.Threading.Tasks;
using MultiClouding.Interfaces;
using MultiClouding.Services;
using MultiClouding.Views;
using ReactiveUI;

namespace MultiClouding.ViewModels;

public class DropBoxRegisterViewModel : ServiceRegisterViewModelBase
{
    public override string ServiceName { get; set; } = "DropBox";
    public override object WindowType { get; set; } = typeof(DropBoxRegisterWindow);
    public override ICloudService Service { get; set; }

    private bool _hasFinished;

    public bool HasFinished
    {
        get => _hasFinished;
        set => this.RaiseAndSetIfChanged(ref _hasFinished, value);
    }

    public DropBoxRegisterViewModel()
    {
        LoginCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await _authenticate();
        });
        CloseCommand = ReactiveCommand.Create(() =>
        {
            return Service;
        });
    }

    private async Task _authenticate()
    {
        Service = await new DropBoxService().Authenticate();
        HasFinished = true;
    }
    public ReactiveCommand<Unit, Unit> LoginCommand { get; set; }
    public ReactiveCommand<Unit, ICloudService> CloseCommand { get; set; }
}
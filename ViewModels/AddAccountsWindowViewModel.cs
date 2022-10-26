using System;
using System.Collections.ObjectModel;
using System.Reactive;
using MultiClouding.Services;
using ReactiveUI;

namespace MultiClouding.ViewModels;

public class AddAccountsWindowViewModel : ViewModelBase
{
    private ObservableCollection<CloudServiceViewModel> _services;

    public ObservableCollection<CloudServiceViewModel> Services
    {
        get => _services;
        set => this.RaiseAndSetIfChanged(ref _services, value);
    }
    public AddAccountsWindowViewModel()
    {
        Services = new ObservableCollection<CloudServiceViewModel>();
        AddGoogleDriveCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var service = await new GoogleDriveService().Authenticate();
            var serviceViewModel = new CloudServiceViewModel(service);
            Services.Add(serviceViewModel);
        });
        AddMicrosoftOneDriveCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var service = await new MicrosoftOneDriveService().Authenticate();
            var serviceViewModel = new CloudServiceViewModel(service);
            Services.Add(serviceViewModel);
        });
        AddDropBoxCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var service = await new DropBoxService().Authenticate();
            var serviceViewModel = new CloudServiceViewModel(service);
            Services.Add(serviceViewModel);
        });
    }
    public ReactiveCommand<Unit, Unit> AddGoogleDriveCommand { get; set; }
    public ReactiveCommand<Unit, Unit> AddMicrosoftOneDriveCommand { get; set; }
    public ReactiveCommand<Unit, Unit> AddDropBoxCommand { get; set; }
}
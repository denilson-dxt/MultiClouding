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
            var service = await new MicrosoftOneDriveService().Authenticate();
            var serviceViewModel = new CloudServiceViewModel(service);
            Services.Add(serviceViewModel);
        });
    }
    public ReactiveCommand<Unit, Unit> AddGoogleDriveCommand { get; set; }
}
using System;
using System.Reactive;
using MultiClouding.Services;
using ReactiveUI;

namespace MultiClouding.ViewModels;

public class AddAccountsWindowViewModel : ViewModelBase
{
    public AddAccountsWindowViewModel()
    {
        AddGoogleDriveCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var service = await new GoogleDriveService().Authenticate();
            var serviceViewModel = new CloudServiceViewModel(service);
            
            
        });
    }
    public ReactiveCommand<Unit, Unit> AddGoogleDriveCommand { get; set; }
}
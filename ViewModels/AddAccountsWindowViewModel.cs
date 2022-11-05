using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using MultiClouding.Services;
using MultiClouding.Views;
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
        ShowLoginMegaWindow = new Interaction<LoginMegaAccountViewModel, Unit>();

        AddGoogleDriveCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var service = await new GoogleDriveService().Authenticate();
            var serviceViewModel = new CloudServiceViewModel(service);
            await service.GetUserInfo();
            Services.Add(serviceViewModel);
        });
        AddMicrosoftOneDriveCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var service = await new MicrosoftOneDriveService().Authenticate();
            var serviceViewModel = new CloudServiceViewModel(service);
            await service.GetUserInfo();
            Services.Add(serviceViewModel);
        });
        AddMegaCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var loginMega = new LoginMegaAccountViewModel();
            var credentials = await ShowLoginMegaWindow.Handle(loginMega);
            var serviceModel = new CloudServiceViewModel(loginMega.Service);
            await serviceModel.Service.GetUserInfo();
            Services.Add(serviceModel);
        });
        AddDropBoxCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var service = await new DropBoxService().Authenticate();
            var serviceViewModel = new CloudServiceViewModel(service);
            await service.GetUserInfo();
            Services.Add(serviceViewModel);
        });

    }
    public ReactiveCommand<Unit, Unit> AddGoogleDriveCommand { get; set; }
    public ReactiveCommand<Unit, Unit> AddMicrosoftOneDriveCommand { get; set; }
    public ReactiveCommand<Unit, Unit> AddMegaCommand { get; set; }
    public ReactiveCommand<Unit, Unit> AddDropBoxCommand { get; set; }

   
    public Interaction<LoginMegaAccountViewModel, Unit> ShowLoginMegaWindow { get; set; }
        
   

}
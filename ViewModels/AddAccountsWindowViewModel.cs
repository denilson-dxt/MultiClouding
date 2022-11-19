using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using MultiClouding.Interfaces;
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

    private ObservableCollection<ServiceRegisterViewModelBase> _registers = new ObservableCollection<ServiceRegisterViewModelBase>()
    {
        new AddGoogleAccountWindowViewModel(),
        new LoginMegaAccountViewModel()
    };

    public ObservableCollection<ServiceRegisterViewModelBase> Registers
    {
        get => _registers;
        set => this.RaiseAndSetIfChanged(ref _registers, value);
    }
    private ServiceRegisterViewModelBase _selectedRegister;

    public ServiceRegisterViewModelBase SelectedRegister
    {
        get => _selectedRegister;
        set => this.RaiseAndSetIfChanged(ref _selectedRegister, value);
    }

    public AddAccountsWindowViewModel()
    {
        Services = new ObservableCollection<CloudServiceViewModel>();
        ShowLoginMegaWindow = new Interaction<LoginMegaAccountViewModel, Unit>();

        AddGoogleDriveCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            // var service = await new GoogleDriveService().Authenticate();
            // var serviceViewModel = new CloudServiceViewModel(service);
            // Services.Add(serviceViewModel);
            //await AuthenticateServiceCommand.Execute(new AddGoogleAccountWindowViewModel());
        });
        AddMicrosoftOneDriveCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var service = await new MicrosoftOneDriveService().Authenticate();
            var serviceViewModel = new CloudServiceViewModel(service);
            Services.Add(serviceViewModel);
        });
        AddMegaCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            // var loginMega = new LoginMegaAccountViewModel();
            // var credentials = await ShowLoginMegaWindow.Handle(loginMega);
            // var serviceModel = new CloudServiceViewModel(loginMega.Service);
            // Services.Add(serviceModel);
            //await AuthenticateServiceCommand.Execute(new LoginMegaAccountViewModel());
        });
        AddDropBoxCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var service = await new DropBoxService().Authenticate();
            var serviceViewModel = new CloudServiceViewModel(service);
            Services.Add(serviceViewModel);
        });

        ShowAuthWindow = new Interaction<ServiceRegisterViewModelBase, ICloudService>();
        AuthenticateServiceCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var service = await ShowAuthWindow.Handle(SelectedRegister);
            if (service is null) return;
            Services.Add(new CloudServiceViewModel(service));
        });

    }
    public ReactiveCommand<Unit, Unit> AddGoogleDriveCommand { get; set; }
    public ReactiveCommand<Unit, Unit> AddMicrosoftOneDriveCommand { get; set; }
    public ReactiveCommand<Unit, Unit> AddMegaCommand { get; set; }
    public ReactiveCommand<Unit, Unit> AddDropBoxCommand { get; set; }
    
    
    public Interaction<LoginMegaAccountViewModel, Unit> ShowLoginMegaWindow { get; set; }
    public Interaction<ServiceRegisterViewModelBase, ICloudService> ShowAuthWindow { get; set; }
    
    public ReactiveCommand<Unit, Unit> AuthenticateServiceCommand { get; set; }
        
   

}
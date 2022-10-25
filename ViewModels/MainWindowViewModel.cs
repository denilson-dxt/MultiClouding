using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using MultiClouding.Enums;
using ReactiveUI;

namespace MultiClouding.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<CloudServiceViewModel> _services;
        public ObservableCollection<CloudServiceViewModel> Services
        {
            get => _services;
            set => this.RaiseAndSetIfChanged(ref _services, value);
        }

        private CloudServiceViewModel _selectedService;

        public CloudServiceViewModel SelectedService
        {
            get => _selectedService;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedService, value);
                if (SelectedService != null)
                {
                    Files = new ObservableCollection<FileViewModel>();
                    Folders = new ObservableCollection<FileViewModel>();
                    _getFiles();
                }
                
            }
        }

        private ObservableCollection<FileViewModel> _folders = new ObservableCollection<FileViewModel>();

        public ObservableCollection<FileViewModel> Folders
        {
            get => _folders;
            set => this.RaiseAndSetIfChanged(ref _folders, value);
        }
        
        private ObservableCollection<FileViewModel> _files = new ObservableCollection<FileViewModel>();

        public ObservableCollection<FileViewModel> Files
        {
            get => _files;
            set => this.RaiseAndSetIfChanged(ref _files, value);
        }
        public MainWindowViewModel()
        {
            Services = new ObservableCollection<CloudServiceViewModel>();
            ShowAddAccountsWindow = new Interaction<AddAccountsWindowViewModel, Unit>();

            Task.Run(()=>
            {
                Dispatcher.UIThread.InvokeAsync(async () =>
                {
                    var add = new AddAccountsWindowViewModel();
                    var r = await ShowAddAccountsWindow.Handle(add);
                    Services = add.Services;
                    if (Services.Count > 0)
                    {
                        SelectedService = Services.First();
                    }
                });
            });


        }
        
        private async Task _getFiles()
        {
            var files = await SelectedService.Service.GetFiles();
            foreach (var file in files)
            {
                var fileViewModel = new FileViewModel(file);
                if(fileViewModel.Type == CloudFileType.File)
                    Files.Add(fileViewModel);
                else
                    Folders.Add(fileViewModel);
            }
        }
        
        public Interaction<AddAccountsWindowViewModel, Unit> ShowAddAccountsWindow { get; set; }
        public ReactiveCommand<Unit, Unit> Test { get; set; }
    }
}
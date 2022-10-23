using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using ReactiveUI;

namespace MultiClouding.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private ObservableCollection<FileViewModel> _files = new ObservableCollection<FileViewModel>()
        {
            new FileViewModel(){Name = "The hidden.mp4", Size = "1.2 GB", LastModified = DateTime.Now},
            new FileViewModel(){Name = "The hidden.mp4", Size = "1.2 GB", LastModified = DateTime.Now},
            new FileViewModel(){Name = "The hidden.mp4", Size = "1.2 GB", LastModified = DateTime.Now},
            new FileViewModel(){Name = "David Guetta - lovers.mp3", Size = "1.2 GB", LastModified = DateTime.Now},
            new FileViewModel(){Name = "script.js", Size = "1.2 GB", LastModified = DateTime.Now},
            new FileViewModel(){Name = "Avatar.png", Size = "0.2 MB", LastModified = DateTime.Now},
            new FileViewModel(){Name = "Avatar.png", Size = "3.2 MB", LastModified = DateTime.Now},
            new FileViewModel(){Name = "Avatar.png", Size = "8.2 MB", LastModified = DateTime.Now},
            new FileViewModel(){Name = "Avatar.png", Size = "2.2 MB", LastModified = DateTime.Now},
            new FileViewModel(){Name = "Avatar.png", Size = "3.2 MB", LastModified = DateTime.Now},
            new FileViewModel(){Name = "Avatar.png", Size = "5.2 MB", LastModified = DateTime.Now},
        };

        public ObservableCollection<FileViewModel> Files
        {
            get => _files;
            set => this.RaiseAndSetIfChanged(ref _files, value);
        }
        public MainWindowViewModel()
        {
            ShowAddAccountsWindow = new Interaction<AddAccountsWindowViewModel, Unit>();

            Task.Run(()=>
            {
                Dispatcher.UIThread.InvokeAsync(async () =>
                {
                    var add = new AddAccountsWindowViewModel();
                    var r = await ShowAddAccountsWindow.Handle(add);
                });
            });


        }

        private async Task GetFiles()
        {
            UserCredential credential;
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.ReadWrite))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets,
                    new[] {"https://www.googleapis.com/auth/drive.readonly"}, "user", CancellationToken.None);
            }

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Multi-clouding"
            });
            var files = service.Files.List().Execute();
            
        }
        
        public Interaction<AddAccountsWindowViewModel, Unit> ShowAddAccountsWindow { get; set; }
        public ReactiveCommand<Unit, Unit> Test { get; set; }
    }
}
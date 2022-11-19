using System;
using System.Diagnostics;
using System.Reactive;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using MultiClouding.Interfaces;
using MultiClouding.Services;
using MultiClouding.Views;
using ReactiveUI;

namespace MultiClouding.ViewModels;

public class MicrosoftOneDriveRegisterViewModel : ServiceRegisterViewModelBase
{
    public override string ServiceName { get; set; } = "Microsoft One Drive";
    public override object WindowType { get; set; } = typeof(MicrosoftOneDriveRegisterWindow);
    public override ICloudService Service { get; set; }

    private string _code = "Click on login to get the code";
    public string Code
    {
        get => _code;
        set => this.RaiseAndSetIfChanged(ref _code, value);
    }

    private bool _hasFinished;

    public bool HasFinished
    {
        get => _hasFinished;
        set => this.RaiseAndSetIfChanged(ref _hasFinished, value);
    }
    public MicrosoftOneDriveRegisterViewModel()
    {
        CloseCommand = ReactiveCommand.Create(() =>
        {
            return Service;
        });
        LoginCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await _authenticate();
        });
    }

    private void _openBrowser()
    {
        System.Diagnostics.Process.Start("xdg-open", "https://www.microsoft.com/link ");
    }
    private async Task _authenticate()
    {
        string[] scopes = {"User.Read"};
        var clientId = "a1caf8f9-f772-4138-9546-7145a34d2d05";
        var tenantId = "f8cdef31-a31e-4b4a-93e4-5f571e91255a";
        var authTenant = "consumers";
        var deviceAuth = new DeviceCodeCredential((info, cancel) =>
        {
            _openBrowser();
            Console.WriteLine(info.Message);
            var regex = new Regex(@"\w*\d\w*");
            var match = regex.Match(info.Message);
            Code = match.Value;
            return Task.FromResult(0);
        }, authTenant, clientId);
        await App.Current.Clipboard.SetTextAsync(Code);
        var graphServiceClient = new GraphServiceClient(deviceAuth, scopes); // you can pass the TokenCredential directly to the GraphServiceClient
        var token  = await deviceAuth.GetTokenAsync(new TokenRequestContext(scopes));
        HasFinished = true;
        
        Service = new MicrosoftOneDriveService().Authenticate(graphServiceClient, token);
    }
    public ReactiveCommand<Unit, ICloudService> CloseCommand { get; set; }
    public ReactiveCommand<Unit, Unit> LoginCommand { get; set; }
}
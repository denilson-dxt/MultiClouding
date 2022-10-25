using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MultiClouding.ViewModels;
using ReactiveUI;

namespace MultiClouding.Views;
public partial class LoginMegaAccount : ReactiveWindow<LoginMegaAccountViewModel>
{
    public LoginMegaAccount()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
        this.WhenActivated(d => d(ViewModel!.LoginCommand.Subscribe(Close)));

#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }


}
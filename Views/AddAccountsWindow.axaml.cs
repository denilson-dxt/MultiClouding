using System;
using System.Linq;
using System.Reactive;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MultiClouding.Interfaces;
using MultiClouding.ViewModels;
using ReactiveUI;

namespace MultiClouding.Views;

public partial class AddAccountsWindow : ReactiveWindow<AddAccountsWindowViewModel>
{
    public AddAccountsWindow()
    {
        InitializeComponent();
        this.WhenActivated(d => d(ViewModel!.ShowLoginMegaWindow.RegisterHandler(DoShowLoginWindow)));
        this.WhenActivated(d => d(ViewModel!.ShowAuthWindow.RegisterHandler(DoShowAuthWindow)));
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async Task DoShowLoginWindow(InteractionContext<LoginMegaAccountViewModel, Unit> interactionContext)
    {
        var window = new LoginMegaAccount();
        window.DataContext = interactionContext.Input;
        await window.ShowDialog(this);
        interactionContext.SetOutput(Unit.Default);
    }

    private async Task DoShowAuthWindow(InteractionContext<ServiceRegisterViewModelBase, ICloudService> context)
    {
        var baseType = context.Input.WindowType;
        var assembly = Assembly.GetExecutingAssembly();
        var type = assembly.GetTypes().First(t => baseType == t);
        var window = Activator.CreateInstance(type) as Window;
        window.DataContext = context.Input;
        await window.ShowDialog(this);
        context.SetOutput(context.Input.Service);
    }
}
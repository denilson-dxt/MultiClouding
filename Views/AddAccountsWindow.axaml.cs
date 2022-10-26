using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MultiClouding.ViewModels;
using ReactiveUI;

namespace MultiClouding.Views;

public partial class AddAccountsWindow : ReactiveWindow<AddAccountsWindowViewModel>
{
    public AddAccountsWindow()
    {
        InitializeComponent();
        this.WhenActivated(d => d(ViewModel!.ShowLoginMegaWindow.RegisterHandler(DoShowLoginWindow)));
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
}
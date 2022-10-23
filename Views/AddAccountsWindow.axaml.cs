using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MultiClouding.ViewModels;

namespace MultiClouding.Views;

public partial class AddAccountsWindow : ReactiveWindow<AddAccountsWindowViewModel>
{
    public AddAccountsWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
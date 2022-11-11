using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MultiClouding.ViewModels;
using ReactiveUI;

namespace MultiClouding.Views;

public partial class AddGoogleDriveAccountWindow : ReactiveWindow<AddGoogleAccountWindowViewModel>
{
    public AddGoogleDriveAccountWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
        this.WhenActivated(d => d(ViewModel!.CloseCommand.Subscribe(Close)));
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
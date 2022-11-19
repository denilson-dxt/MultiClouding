using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MultiClouding.ViewModels;
using ReactiveUI;

namespace MultiClouding.Views;

public partial class DropBoxRegisterWindow : ReactiveWindow<DropBoxRegisterViewModel>
{
    public DropBoxRegisterWindow()
    {
        InitializeComponent();
        this.WhenActivated(d => d(ViewModel!.CloseCommand.Subscribe(Close)));
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
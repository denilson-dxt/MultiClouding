using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using MultiClouding.ViewModels;
using ReactiveUI;

namespace MultiClouding.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(d => d(this.ViewModel!.ShowAddAccountsWindow.RegisterHandler(ShowAddAccountsWindow)));
        }

        private void Window_OnKeyDown(object? sender, KeyEventArgs e)
        {
            if(e.Key == Key.F11)
                WindowState = WindowState != WindowState.FullScreen ? WindowState.FullScreen : WindowState.Normal;
        }

        private async Task ShowAddAccountsWindow(InteractionContext<AddAccountsWindowViewModel, Unit> interactionContext)
        {
            var addAccountsWindow = new AddAccountsWindow();
            addAccountsWindow.DataContext = interactionContext.Input;
            await addAccountsWindow.ShowDialog(this);
            interactionContext.SetOutput(Unit.Default);
        }
    }
}
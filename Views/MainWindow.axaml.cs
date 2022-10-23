using Avalonia.Controls;
using Avalonia.Input;

namespace MultiClouding.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_OnKeyDown(object? sender, KeyEventArgs e)
        {
            if(e.Key == Key.F11)
                WindowState = WindowState != WindowState.FullScreen ? WindowState.FullScreen : WindowState.Normal;
        }
    }
}
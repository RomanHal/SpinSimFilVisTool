using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GUIProject.ViewModels;

namespace GUIProject.Views
{
    public class PrintScreenOptions : Window
    {
        public PrintScreenOptions()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public PrintScreenOptions(string defaultPath)
        {
            this.InitializeComponent(defaultPath);
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent(string defaultPath=null)
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = new PrintScreenOptionsViewModel(defaultPath) {Close=Close,Cancel=Close };
        }
    }
}

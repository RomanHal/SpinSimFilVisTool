using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GUIProject.ViewModels;

namespace GUIProject.Views
{
    public class TextWindow : Window
    {
        public TextWindow()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        public TextWindow(params string[] text)
        {
            this.InitializeComponent(text);
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent(string[] text=null)
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = new TextWindowViewModel(text) { Close = Close };
        }
    }
}

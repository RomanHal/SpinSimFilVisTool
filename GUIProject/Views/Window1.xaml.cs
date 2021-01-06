using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GUIProject.ViewModels;

namespace GUIProject.Views
{
    public class Window1 : Window
    {

        public Window1()
        {
            this.InitializeComponent();
           
           
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = new W1VM(this);
        }
    }
}

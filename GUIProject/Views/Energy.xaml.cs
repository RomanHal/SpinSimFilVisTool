using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GUIProject.ViewModels;

namespace GUIProject.Views
{
    public class Energy : Window
    {
        public Energy()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = new EnergyViewModel() { CloseAction = this.Close };
        }
    }
}

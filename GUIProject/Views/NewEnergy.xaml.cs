using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GUIProject.Views
{
    public class NewEnergy : Window
    {
        public NewEnergy()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

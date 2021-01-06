using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GUIProject.ViewModels;

namespace GUIProject.Views
{
    public class UnInterpretedText : Window
    {
        public UnInterpretedText()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = new UnInterpretedTextViewModel() {Close=this.Close };
        }
    }
}

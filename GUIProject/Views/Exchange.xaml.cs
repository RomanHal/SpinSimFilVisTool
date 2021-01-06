using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CoreLibrary.Interfaces;
using GUIProject.ViewModels;

namespace GUIProject.Views
{
    public class Exchange : Window
    {
        public Exchange()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        public Exchange(IEnergy energy)
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent(IEnergy energy=null)
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = new ExchangeViewModel(energy) { Close = Close };
        }
    }
}

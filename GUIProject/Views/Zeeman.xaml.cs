using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CoreLibrary.Interfaces;
using GUIProject.ViewModels;

namespace GUIProject.Views
{
    public class Zeeman : Window
    {
        public Zeeman()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public Zeeman(IEnergy energy)
        {
            this.InitializeComponent(energy);
#if DEBUG
            this.AttachDevTools();
#endif
        }


        private void InitializeComponent(IEnergy energy=null)
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = new ZeemanViewModel(energy) { Close = Close };
        }
    }
}

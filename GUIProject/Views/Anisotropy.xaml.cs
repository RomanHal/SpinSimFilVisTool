using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CoreLibrary.Interfaces;
using GUIProject.ViewModels;

namespace GUIProject.Views
{
    public class Anisotropy : Window
    {
        public Anisotropy()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        public Anisotropy(IEnergy energy)
        {
            InitializeComponent(energy);
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent(IEnergy energy=null)
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = new AnisotropyViewModel(energy) { Close = this.Close };
        }
    }
}

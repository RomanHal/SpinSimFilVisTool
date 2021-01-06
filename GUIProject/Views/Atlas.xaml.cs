using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CoreLibrary.Interfaces;
using GUIProject.ViewModels;

namespace GUIProject.Views
{
    public class Atlas : Window
    {
        public Atlas()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        public Atlas(IObject @object):this()
        {
            ((AtlasViewModel)DataContext).SelectedAtlas.Value = @object.Name;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = new AtlasViewModel() { Close = this.Close };
        }
    }
}

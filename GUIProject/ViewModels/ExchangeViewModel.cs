using Avalonia.Controls;
using Avalonia.Media;
using CoreLibrary.Interfaces;
using GUIProject.Models;
using GUIProject.Other;
using GUIProject.Views;
using System;
using System.Collections.ObjectModel;

namespace GUIProject.ViewModels
{
    public class ExchangeViewModel:ViewModelBase
    {
        public ObservableCollection<string> Atlases { get; set; }
        public ObservableCollection<string> Regions { get; set; }

        private const string _surface = "Surface ";

        public GuiProperty<string> SelectedSurfaceString => new GuiProperty<string>(_surface + _selectedSurface.Value);
        public GuiProperty<string> Name { get => _name; set => _name=value; }
        public GuiProperty<double> Sigma { get => _sigma; set => _sigma = value; }
        public GuiProperty<string> SelectedAtlas { get => _selectedAtlas; set => _selectedAtlas=value; }
        public GuiProperty<string> SelectedRegion { get => _selectedRegion; set => _selectedRegion = value; }
        public GuiProperty<double> Norm { get => _norm; set => _norm = value; }
        public GuiProperty<double> VectorX { get => _vectorX; set => _vectorX = value; }
        public GuiProperty<double> VectorY { get => _vectorY; set => _vectorY = value; }
        public GuiProperty<double> VectorZ { get => _vectorZ; set => _vectorZ = value; }
        public GuiProperty<double> Value { get => _value; set => _value = value; }
        public GuiProperty<bool> Sign { get => _sign; set => _sign = value; }
        public GuiProperty<int> SelectedSurface { get => _selectedSurface; set => _selectedSurface = value; }
        public GuiProperty<SolidColorBrush> ColorBrush { get => _colorBrush; set => _colorBrush = value; }

        public Action Close { get; set; }

        
        GuiProperty<string> _name= new GuiProperty<string>();
        GuiProperty<double> _sigma = new GuiProperty<double>();
        GuiProperty<string> _selectedAtlas = new GuiProperty<string>();
        GuiProperty<string> _selectedRegion = new GuiProperty<string>();
        GuiProperty<double> _norm = new GuiProperty<double>();
        GuiProperty<double> _vectorX = new GuiProperty<double>();
        GuiProperty<double> _vectorY = new GuiProperty<double>();
        GuiProperty<double> _vectorZ = new GuiProperty<double>();
        GuiProperty<double> _value = new GuiProperty<double>();
        GuiProperty<bool> _sign = new GuiProperty<bool>();
        GuiProperty<int> _selectedSurface = new GuiProperty<int>(1);
        GuiProperty<SolidColorBrush> _colorBrush = new GuiProperty<SolidColorBrush>();
        IViewCreator _viewCreator = new ViewLocator();
        ExchangeModel _model = new ExchangeModel();
        public ExchangeViewModel(IEnergy energy):this()
        {

        }
        public ExchangeViewModel()
        {
            _selectedSurface.PropertyChanged += _selectedSurface_PropertyChanged;
            _selectedAtlas.PropertyChanged += _selectedAtlas_PropertyChanged;
        }

        public void Apply()
        {
            throw new NotImplementedException();
        }

        
        public void ChangeSurface()
        {
            if (SelectedSurface.Value < 2) SelectedSurface.Value = 2;
            else SelectedSurface.Value = 1;
        }
        

        public void Back()
        {
            Window window = (Window)_viewCreator.Build(new EnergyViewModel());
            window.Show();
            Close();
        }
        public async void SelectColor()
        {
            var color = await new ColorDialog().ShowDialog<Color?>();
            if (color.HasValue)
            {
                ColorBrush.Value = new SolidColorBrush(color.Value);
            }
        }

        private void SetRegions()
        {
            throw new NotImplementedException();
        }

        private void _selectedSurface_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SelectedSurfaceString.Value = _surface + _selectedSurface.Value;
            // SetProperties();
        }

        private void SetProperties()
        {
            throw new NotImplementedException();
        }

        private Color GetColor(IColor color)
        {
            return new Color(255, color.R, color.G, color.B);
        }

        private void _selectedAtlas_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SetRegions();
        }

    }
}

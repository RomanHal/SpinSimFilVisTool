using Avalonia.Controls;
using Avalonia.Media;
using CoreLibrary;
using CoreLibrary.Interfaces;
using GUIProject.Models;
using GUIProject.Other;
using GUIProject.Views;
using MifParser.Factories;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;

namespace GUIProject.ViewModels
{
    public class AnisotropyViewModel:ViewModelBase
    {
        private const string defaultRegion = "default";
        bool _newEnergy;
        public Action Close { set; private get; }
        IViewCreator _viewCreator = new ViewLocator();
        AnisotropyModel _model = new AnisotropyModel();
        public GuiProperty<string> Name { get=>_name; set=>_name=value; }
        GuiProperty<string> _name = new GuiProperty<string>();

        public ObservableCollection<string> Atlases => GetAtlases();

        public GuiProperty<string> SelectedAtlas { get=>_selectedAtlas; set=>_selectedAtlas=value; }
        GuiProperty<string> _selectedAtlas = new GuiProperty<string>();
        public ObservableCollection<string> Regions { get; set; }

        public GuiProperty<string> SelectedRegion { get => _selectedRegion; set => _selectedRegion = value; }
        GuiProperty<string> _selectedRegion = new GuiProperty<string>();
        public GuiProperty<double> Value { get => _value; set => _value = value; }
        GuiProperty<double> _value = new GuiProperty<double>();
        public GuiProperty<double> VectorX { get => _vectorX; set => _vectorX = value; }
        GuiProperty<double> _vectorX = new GuiProperty<double>();
        public GuiProperty<double> VectorY { get => _vectorY; set => _vectorY = value; }
        GuiProperty<double> _vectorY = new GuiProperty<double>();
        public GuiProperty<double> VectorZ { get => _vectorZ; set => _vectorZ = value; }
        GuiProperty<double> _vectorZ = new GuiProperty<double>();

        public GuiProperty<double> PointX { get => _pointX; set => _pointX = value; }
        GuiProperty<double> _pointX = new GuiProperty<double>();
        public GuiProperty<double> PointY { get => _pointY; set => _pointY = value; }
        GuiProperty<double> _pointY = new GuiProperty<double>();
        public GuiProperty<double> PointZ { get => _pointZ; set => _pointZ = value; }
        GuiProperty<double> _pointZ = new GuiProperty<double>();

        public GuiProperty<bool> Visible { get => _visible; set => _visible = value; }
        public GuiProperty<bool> _visible = new GuiProperty<bool>();

        public GuiProperty<SolidColorBrush> ColorBrush { get => _colorBrush;
            set => _colorBrush = value; }
        GuiProperty<SolidColorBrush> _colorBrush = new GuiProperty<SolidColorBrush>();

        public GuiProperty<bool> NewEnergyPart { get => _newEnergyPart; set => _newEnergyPart = value; }
        GuiProperty<bool> _newEnergyPart=new GuiProperty<bool>();

        public GuiProperty<bool> EnableSelectRegion { get => _enableSelectRegion; set => _enableSelectRegion = value; }
        GuiProperty<bool> _enableSelectRegion = new GuiProperty<bool>();

        public AnisotropyViewModel(IEnergy energy):this()
        {
            if(energy!=null)
            {
                _model.SelectedEnergy = energy as SpintronicsAnisotropy;
                _newEnergy = false;
                SetRegions();
                SelectedAtlas.Value = _model.SelectedEnergy.Atlas ?? "None";
                SelectedRegion.Value = _model.SelectedEnergy.Regions[0];
                SetProperties(0);
                
            }
            else _newEnergy = true;
        }



        public AnisotropyViewModel()
        {
            SelectedRegion.PropertyChanged += SelectedRegionPropertyChanged;
            NewEnergyPart.PropertyChanged += NewEnergyPartPropertyChanged;
            SelectedAtlas.PropertyChanged += SelectedAtlas_PropertyChanged;
            EnableSelectRegion.Value = true;
        }

        private void SelectedAtlas_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SetRegions();
            if(SelectedAtlas.Value=="None")
            {
                SelectedRegion.Value=defaultRegion;
                EnableSelectRegion.Value = false;
            }
            else
            {
                EnableSelectRegion.Value = true;
            }
        }

        private void SelectedRegionPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(NewEnergyPart.Value==false)
            {
               SetProperties(Regions.IndexOf(SelectedRegion.Value));
            }
            
        }


        private void SetProperties(int index)
        {
            Name.Value = _model.SelectedEnergy.Name;
            Value.Value = _model.SelectedEnergy.Values[index];
            VectorX.Value = _model.SelectedEnergy.DirectionX[index];
            VectorY.Value = _model.SelectedEnergy.DirectionY[index];
            VectorZ.Value = _model.SelectedEnergy.DirectionZ[index];
            PointX.Value = _model.SelectedEnergy.PositionX[index];
            PointY.Value = _model.SelectedEnergy.PositionY[index];
            PointZ.Value = _model.SelectedEnergy.PositionZ[index];
            ColorBrush.Value = new SolidColorBrush(GetColor(_model.SelectedEnergy.Color));
            Visible.Value = _model.SelectedEnergy.Visibility[index];
        }

        private Color GetColor(IColor color)
        {
            return new Color(255, color.R, color.G, color.B);
        }

        private void NewEnergyPartPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SetRegions();
        }

        private void SetRegions()
        {
            if (NewEnergyPart.Value == true)
            {
                Regions = new ObservableCollection<string>(_model.Regions);
            }
            else
            {
                if(_model.SelectedEnergy==null)
                {
                    Regions = new ObservableCollection<string>() { defaultRegion };
                }
                else
                {
                    Regions = new ObservableCollection<string>(_model.SelectedEnergy.Regions);
                }
            }
            this.RaisePropertyChanged(nameof(Regions));
        }

        public async void SelectColor()
        {
            var color = await new ColorDialog().ShowDialog<Color?>();
            if (color.HasValue)
            {
                ColorBrush.Value = new SolidColorBrush(color.Value);
            }
        }

        public void Apply()
        {
            if (_newEnergy) Create();
            else if (NewEnergyPart.Value) AddEnergyPart() ;
            else Update();
            this.RaisePropertyChanged(nameof(Regions));
            NewEnergyPart.Value = false;
        }

        private void AddEnergyPart()
        {
            _model.AddEnergyPart(VectorX.Value,VectorY.Value,VectorZ.Value, new GuiColor(ColorBrush.Value.Color),Value.Value,
                SelectedRegion.Value);
        }

        private void Update()
        {
            var index = Regions.IndexOf(SelectedRegion.Value);
            _model.UpdateEnergyPart(index, Name.Value,  SelectedAtlas.Value, SelectedRegion.Value, Value.Value, VectorX.Value, 
                VectorY.Value, VectorZ.Value, PointX.Value, PointY.Value, PointZ.Value, new GuiColor(ColorBrush.Value.Color),
                Visible.Value);
        }

        private void Create()
        {

            _model.Add(Name.Value, SelectedRegion.Value, SelectedAtlas.Value, Value.Value, PointX.Value,
                PointY.Value, PointZ.Value, VectorX.Value, VectorY.Value, VectorZ.Value,
                new GuiColor(ColorBrush.Value.Color));
            _newEnergy = false;
        }

        public void Delete()
        {
            _model.DeleteEnergyPart(Regions.IndexOf(SelectedRegion.Value));
        }
        public void Back()
        {
            Window window =(Window) _viewCreator.Build(new EnergyViewModel());
            window.Show();
            Close();
        }

        public void AddRegion()
        {
            if(SelectedAtlas.Value!="None")
            NewEnergyPart.Value = true;
        }
        public void CancelAddRegion()
        {
            NewEnergyPart.Value = false;
        }
        private ObservableCollection<string> GetAtlases()
        {
            var collection= new ObservableCollection<string>(_model.Atlases);
            collection.Insert(0, "None");
            return collection;
        }


    }
}

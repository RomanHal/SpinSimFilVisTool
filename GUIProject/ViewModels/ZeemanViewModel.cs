using Avalonia.Controls;
using Avalonia.Media;
using CoreLibrary;
using CoreLibrary.Interfaces;
using GUIProject.Models;
using GUIProject.Other;
using GUIProject.Views;
using System;

namespace GUIProject.ViewModels
{
    public class ZeemanViewModel:ViewModelBase
    {
        public GuiProperty<string> Name { get => _name; set => _name = value; }
        public GuiProperty<double> Multiplier { get => _multiplier; set => _multiplier = value; }
        public GuiProperty<int> Steps { get => _steps; set => _steps = value; }
        public GuiProperty<double> StartFieldX { get => _startFieldX; set => _startFieldX = value; } 
        public GuiProperty<double> StartFieldY { get => _startFieldY; set => _startFieldY = value; }
        public GuiProperty<double> StartFieldZ { get => _startFieldZ; set => _startFieldZ = value; }
        public GuiProperty<double> EndFieldX { get => _endFieldX; set => _endFieldX = value; }
        public GuiProperty<double> EndFieldY { get => _endFieldY; set => _endFieldY = value; }
        public GuiProperty<double> EndFieldZ { get => _endFieldZ; set => _endFieldZ=value; }

        public GuiProperty<double> PointX { get => _pointX; set => _pointX = value; }
        public GuiProperty<double> PointY { get => _pointY; set => _pointY = value; }
        public GuiProperty<double> PointZ { get => _pointZ; set => _pointZ = value; }

        public GuiProperty<SolidColorBrush> ColorBrush { get => _colorBrush; set => _colorBrush = value; }

        public GuiProperty<bool> Visible { get => _visible; set => _visible = value; }
        public GuiProperty<bool> NewEnergyPart { get => _newEnergyPart; set => _newEnergyPart = value; }

        GuiProperty<bool> _visible = new GuiProperty<bool>();
        GuiProperty<bool> _newEnergyPart = new GuiProperty<bool>();
        GuiProperty<string> _name = new GuiProperty<string>();
        GuiProperty<double> _multiplier = new GuiProperty<double>();
        GuiProperty<int> _steps = new GuiProperty<int>();
        GuiProperty<double> _startFieldX = new GuiProperty<double>();
        GuiProperty<double> _startFieldY = new GuiProperty<double>();
        GuiProperty<double> _startFieldZ = new GuiProperty<double>();
        GuiProperty<double> _endFieldX = new GuiProperty<double>();
        GuiProperty<double> _endFieldY = new GuiProperty<double>();
        GuiProperty<double> _endFieldZ = new GuiProperty<double>();

        GuiProperty<double> _pointX = new GuiProperty<double>();
        GuiProperty<double> _pointY = new GuiProperty<double>();
        GuiProperty<double> _pointZ = new GuiProperty<double>();

        GuiProperty<SolidColorBrush> _colorBrush = new GuiProperty<SolidColorBrush>();

        int _selectedPart ;
        IViewCreator _viewCreator = new ViewLocator();


        public Action Close { get; set; }

        ZeemanModel _model = new ZeemanModel();
        bool newEnergy;
        
        public ZeemanViewModel(IEnergy energy)
        {
            if (energy == null) newEnergy = true;
            else
            {
                _model.SelectedEnergy = (SpintronicsZeeman)energy;
                SetProperties(0);
            }
        }
        public ZeemanViewModel()
        {
            newEnergy = true;
        }

        public async void SelectColor()
        {
            var color = await new ColorDialog().ShowDialog<Color?>();
            if (color.HasValue)
            {
                ColorBrush.Value = new SolidColorBrush(color.Value);
            }
        }

        public void Delete()
        {
            _model.DeleteEnergyPart(_selectedPart);
            SetProperties(0);
        }

        public void Apply()
        {
            if (newEnergy) CreateNewEnergy();
            else if (NewEnergyPart.Value) CreateNewPart();
            else Update();
        }
        public void Back()
        { 
            Window window = (Window)_viewCreator.Build(new EnergyViewModel());
            window.Show();
            Close();
        }
        public void Next()
        {
            if(_model.SelectedEnergy.PointX.Length>_selectedPart+1)
            {
                _selectedPart++;
                SetProperties(_selectedPart);
            }
        }
        public void Previous()
        {
            if(_selectedPart>0)
            {
                _selectedPart--;
                SetProperties(_selectedPart);
            }
        }
        public void AddPart()
        {
            _newEnergyPart.Value = true;
        }
        public void CancelAddPart()
        {
            _newEnergyPart.Value = false;
            SetProperties(_selectedPart);
        }
        public void LookAt()
        {
            throw new NotImplementedException();
        }

        private void SetProperties(int partIndex)
        {
            Name.Value = _model.SelectedEnergy.Name;
            Multiplier.Value = _model.SelectedEnergy.Multuplier;
            Steps.Value = _model.SelectedEnergy.Steps[partIndex];
            StartFieldX.Value = _model.SelectedEnergy.StartVectorX[partIndex];
            StartFieldY.Value = _model.SelectedEnergy.StartVectorY[partIndex];
            StartFieldZ.Value = _model.SelectedEnergy.StartVectorZ[partIndex];
            EndFieldX.Value = _model.SelectedEnergy.EndVectorX[partIndex];
            EndFieldY.Value = _model.SelectedEnergy.EndVectorY[partIndex];
            EndFieldZ.Value = _model.SelectedEnergy.EndVectorZ[partIndex];
            Visible.Value = _model.SelectedEnergy.Visibility[partIndex];
            ///Visible2
            ColorBrush.Value = new SolidColorBrush(GetColor(_model.SelectedEnergy.Colors[partIndex]));
        }

        private void Update()
        {
            _model.UpdateEnergyPart(Name.Value, Multiplier.Value, _selectedPart, StartFieldX.Value,
                StartFieldY.Value, StartFieldZ.Value, EndFieldX.Value, EndFieldY.Value, EndFieldZ.Value, PointX.Value,
                PointY.Value, PointZ.Value, Steps.Value, new GuiColor(ColorBrush.Value.Color), Visible.Value, false);
        }

        private void CreateNewPart()
        {
            _model.AddPart(StartFieldX.Value, StartFieldY.Value, StartFieldZ.Value, EndFieldX.Value, EndFieldY.Value,
                EndFieldZ.Value, new GuiColor(ColorBrush.Value.Color), Steps.Value);
        }

        private void CreateNewEnergy()
        {
            _model.Add(Name.Value, Multiplier.Value, Steps.Value, PointX.Value, PointY.Value, PointZ.Value, 
                StartFieldX.Value, StartFieldY.Value, StartFieldZ.Value, EndFieldX.Value, EndFieldY.Value, 
                EndFieldZ.Value, new GuiColor(ColorBrush.Value.Color));
        }
        private Color GetColor(IColor color)
        {
            return new Color(255, color.R, color.G, color.B);
        }
    }
}

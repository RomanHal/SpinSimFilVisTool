using Avalonia.Controls;
using Avalonia.Media;
using CoreLibrary.Enums;
using CoreLibrary.GraphicsServices;
using CoreLibrary.Interfaces;
using GUIProject.Models;
using GUIProject.Other;
using GUIProject.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GUIProject.ViewModels
{
    public class AtlasViewModel : ViewModelBase
    {
        public ObservableCollection<string> Atlases { get=>new ObservableCollection<string>(_atlasModel.Atlases); }
        public GuiProperty<string> SelectedAtlas { get => _selectedAtlas; set => _selectedAtlas = value; }
        GuiProperty<string> _selectedAtlas = new GuiProperty<string>();
        GuiProperty<IObject> _atlas = new GuiProperty<IObject>();
        public GuiProperty<int> AtlasesIndex { get => _atlasesIndex; set => _atlasesIndex = value; }
        GuiProperty<int> _atlasesIndex = new GuiProperty<int>();
        public GuiProperty<string> AtlasName { get => _atlasName; set => _atlasName = value; }
        GuiProperty<string> _atlasName = new GuiProperty<string>() {Value="" };
        public GuiProperty<bool> IsCreatingNew { get => _isCreatingNew; set => _isCreatingNew = value; }
        public GuiProperty<bool> _isCreatingNew=new GuiProperty<bool>();
        public GuiProperty<AtlasType> SelectedAtlasType { get => _selectedAtlasType; set => _selectedAtlasType = value; }
        GuiProperty<AtlasType> _selectedAtlasType = new GuiProperty<AtlasType>();
        public GuiProperty<Array> AtlasType { get => _atlasType; set => _atlasType = value; }
        GuiProperty<Array> _atlasType = new GuiProperty<Array>() { Value = Enum.GetValues(typeof(AtlasType)) };
        public GuiProperty<double> XMin { get=>_xMin; set=>_xMin=value; }
        public GuiProperty<double> XMax { get=>_xMax; set=>_xMax=value; }
        public GuiProperty<double> YMin { get => _yMin; set => _yMin = value; }
        public GuiProperty<double> YMax { get => _yMax; set => _yMax = value; }
        public GuiProperty<double> ZMin { get => _zMin; set => _zMin = value; }
        public GuiProperty<double> ZMax { get => _zMax; set => _zMax = value; }
        GuiProperty<double> _xMin = new GuiProperty<double>();
        GuiProperty<double> _xMax = new GuiProperty<double>();
        GuiProperty<double> _yMin = new GuiProperty<double>();
        GuiProperty<double> _yMax = new GuiProperty<double>();
        GuiProperty<double> _zMin = new GuiProperty<double>();
        GuiProperty<double> _zMax = new GuiProperty<double>();

        public GuiProperty<string> Region { get => _region; set => _region = value; }
        public GuiProperty<string> _region = new GuiProperty<string>();


        public GuiProperty<bool> ScriptAllowed { get=>_scriptAllowed; set=>_scriptAllowed=value; }
        GuiProperty<bool> _scriptAllowed = new GuiProperty<bool>();
        public ObservableCollection<string> Scripts { get => new ObservableCollection<string>(listTest);  }
        ObservableCollection<string> _scripts = new ObservableCollection<string>
            ( );
        List<string> listTest = new List<string>(new string[] { "EllipseX", "EllipseY", "EllipseZ" });
        public GuiProperty<string> SelectedScript { get => _selectedScript; set => _selectedScript = value; }
        GuiProperty<string> _selectedScript= new GuiProperty<string>();
        public GuiProperty<string> ScriptArgs { get => _scriptArgs; set => _scriptArgs=value; }
        GuiProperty<string> _scriptArgs = new GuiProperty<string>();

        GuiProperty<bool> Visible { get => _visible; set => _visible = value; }
        GuiProperty<bool> _visible = new GuiProperty<bool>();

        public GuiProperty<SolidColorBrush> ColorBrush { get=> _colorBrush; set=> _colorBrush=value; }
        GuiProperty<SolidColorBrush> _colorBrush = new GuiProperty<SolidColorBrush>();
        public GuiProperty<bool> ByMs { get => _byMs; set => _byMs = value; }
        GuiProperty<bool> _byMs = new GuiProperty<bool>();

        public GuiProperty<bool> NewMultiAtlas { get => _newMultiAtlas; set => _newMultiAtlas = value; }
        GuiProperty<bool> _newMultiAtlas= new GuiProperty<bool>();

        public ObservableCollection<string> MultiAtlases { get =>new ObservableCollection<string>(_atlasModel.MultiAtlases); }

        public GuiProperty<string> SelectedMultiAtlas { get => _selectedMultiAtlas; set => _selectedMultiAtlas = value; }
        GuiProperty<string> _selectedMultiAtlas= new GuiProperty<string>();

        private AtlasModel _atlasModel = new AtlasModel();

        public Action Close { get; set; }

        public AtlasViewModel()
        {
            SelectedAtlas.PropertyChanged += SelectedAtlas_PropertyChanged;
            SelectedAtlasType.PropertyChanged += SelectedAtlasType_PropertyChanged;

        }


        private void SelectedAtlasType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_selectedAtlasType.Value == CoreLibrary.Enums.AtlasType.ScriptAtlas)
                _scriptAllowed.Value = true;
            else _scriptAllowed.Value = false;
        }

        private void SelectedAtlas_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _newMultiAtlas.Value = false;
            _atlas.Value = _atlasModel.GetAtlas(_selectedAtlas.Value);
            _atlasName.Value = _selectedAtlas.Value;
            _xMin.Value = _atlas.Value.Xmin;
            _xMax.Value = _atlas.Value.Xmax;
            _yMin.Value = _atlas.Value.Ymin;
            _yMax.Value = _atlas.Value.Ymax;
            _zMin.Value = _atlas.Value.Zmin;
            _zMax.Value = _atlas.Value.Zmax;
            _visible.Value = _atlas.Value.Visible;
            _selectedMultiAtlas.Value = _atlas.Value.Multiatlas;
            _selectedAtlasType.Value = _atlas.Value.Type;
            _isCreatingNew.Value = false;
            _colorBrush.Value = new SolidColorBrush(GetColor(_atlas.Value.Color));
            _region.Value = string.Join(' ', _atlas.Value.Regions); 

            
            if (_atlas.Value.Type == CoreLibrary.Enums.AtlasType.ScriptAtlas)
            {
                _scriptAllowed.Value = true;
                _selectedScript.Value = _atlas.Value.Script.Name;
                _scriptArgs.Value = _atlas.Value.ScriptArgs;
                OnPropertyChanged(nameof(Scripts));
            }


        }
        Color GetColor(IColor color)
        {
            return new Color(255, color.R, color.G, color.B);
        }

        public async void SelectColor()
        {
            var color = await new ColorDialog().ShowDialog<Color?>();
            if(color.HasValue)
            {
                ColorBrush.Value = new SolidColorBrush(color.Value);
            }
        }

        public void NewAtlas()
        {
            _isCreatingNew.Value = true;
        }
        public void LookAtSelectedItem()
        {
            new CameraMover().LookAt(_atlasModel.GetAtlas(SelectedAtlas.Value));
        }
        public void CreateNewScript()
        {

        }
        public void Apply()
        {
            if (_isCreatingNew.Value)
                Add();
            else Update();
            OnPropertyChanged(nameof(Atlases));
            OnPropertyChanged(nameof(MultiAtlases));
            _newMultiAtlas.Value = false;
        }

        private void Update()
        {
            if(_selectedAtlas.Value!=null&&_selectedAtlas.Value!="")
            {
                _atlasModel.Update(_selectedAtlas.Value, _atlasName.Value, _xMin.Value, _xMax.Value,
                    _yMin.Value, _yMax.Value, _zMin.Value, _zMax.Value, _colorBrush.Value.Color,
                    _visible.Value,_selectedMultiAtlas.Value);
            }
        }

        private void Add()
        {
            _atlasModel.Add(_atlasName.Value, _selectedAtlasType.Value, _xMin.Value, _xMax.Value, 
                _yMin.Value,_yMax.Value, _zMin.Value, _zMax.Value, _colorBrush.Value.Color, _region.Value, 
                _selectedScript.Value, _scriptArgs.Value,_selectedMultiAtlas.Value);

        }

        public void Delete()
        {
            _atlasModel.Delete(SelectedAtlas.Value);
            _newMultiAtlas.Value = false;
            OnPropertyChanged(nameof(Atlases));
            OnPropertyChanged(nameof(MultiAtlases));
        }
        public void Cancel()
        {
            Close.Invoke();
        }
        public void CreateNewMultiAtlas()
        {
            _newMultiAtlas.Value = true;
        }
        void OnPropertyChanged(string propertyName)
        {
            this.RaisePropertyChanged(propertyName);
        }

        
    }
}

using Avalonia.Controls;
using CoreLibrary.Enums;
using CoreLibrary.GraphicsServices;
using GUIProject.Models;
using GUIProject.Other;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;

namespace GUIProject.ViewModels
{
    public class EnergyViewModel:ViewModelBase
    {
        IViewCreator _viewCreator = new ViewLocator();
        EnergyModel _model = new EnergyModel();
        public ObservableCollection<string> Energies => new ObservableCollection<string>(_model.EnergiesNames);
        public GuiProperty<string> SelectedEnergy { get=>_selectedEnergy; set=>_selectedEnergy=value; }
        GuiProperty<string> _selectedEnergy = new GuiProperty<string>();
        public Array Types => Enum.GetValues(typeof(EnergyType));
        public GuiProperty<EnergyType> SelectedType { get=>_selectedType; set=>_selectedType=value; }
        GuiProperty<EnergyType> _selectedType = new GuiProperty<EnergyType>();

        public GuiProperty<bool> NewEnergy { get=>_newEnergy; set=>_newEnergy=value; }
        GuiProperty<bool> _newEnergy = new GuiProperty<bool>();

        public Action CloseAction { private get; set; }
        public EnergyViewModel()
        {
            _selectedEnergy.PropertyChanged += _selectedEnergy_PropertyChanged;
        }

        private void _selectedEnergy_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var energy = _model.GetEnergy(SelectedEnergy.Value);
            SelectedType.Value = energy.Type;
            NewEnergy.Value = false;
        }

        public void LookAt()
        {
            new CameraMover().LookAt(_model.GetEnergy(SelectedEnergy.Value));
        }
        public void Select()
        {
            if(NewEnergy.Value==false)
            {
                ViewModelBase viewModel;
                switch(SelectedType.Value)
                {
                    case EnergyType.UniaxialAnisotropy:
                        viewModel = new AnisotropyViewModel();
                        break;
                    case EnergyType.UZeeman:
                        viewModel = new ZeemanViewModel();
                        break;
                    case EnergyType.TwoSurfaceExchange:
                        viewModel = new ExchangeViewModel();
                        break;
                    default: throw new FormatException();
                }
                var window = (Window)_viewCreator.Build(viewModel,
                    _model.GetEnergy(SelectedEnergy.Value));
                window.Show();
            }
            else
            {
                ViewModelBase viewModel;
                switch (SelectedType.Value)
                {
                    case EnergyType.UniaxialAnisotropy:
                        viewModel = new AnisotropyViewModel();
                        break;
                    case EnergyType.UZeeman:
                        viewModel = new ZeemanViewModel();
                        break;
                    case EnergyType.TwoSurfaceExchange:
                        viewModel = new ExchangeViewModel();
                        break;
                    default: throw new FormatException();
                }
                var window = (Window)_viewCreator.Build(viewModel);
                window.Show();

            }
            Close();
        }
        public void DeleteEnergy()
        {
            _model.Delete(SelectedEnergy.Value);
            this.RaisePropertyChanged(nameof(Energies));
        }
        public void Close()
        {
            CloseAction?.Invoke();
        }

        public void CreateNewEnergy()
        {
            NewEnergy.Value = true;
        }
    }
}

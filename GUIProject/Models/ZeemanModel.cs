using CoreLibrary;
using CoreLibrary.Enums;
using CoreLibrary.Factories;
using CoreLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GUIProject.Models
{
    class ZeemanModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        EnergyPartController _controller = new EnergyPartController();
        ZeemanFactory _factory = new ZeemanFactory();
        public SpintronicsZeeman SelectedEnergy { get; set; }
        public List<string> Atlases => GetAtlases();

        public List<string> Regions => DataStorage.ObjectContainer.Regions;
        public List<string> EnergiesNames => DataStorage.ObjectContainer.EnergieNames;
        public IEnergy GetEnergy(string name)
        {
            return DataStorage.ObjectContainer.GetEnergy(name);
        }

        public void Add(string name, double multiplier, int steps, double positionX, double positionY,
            double positionZ, double directionX, double directionY, double directionZ, double endDirectionX,
            double endDirectionY, double endDirectionZ, IColor color)
        {
            var energy = _factory.Create(name, multiplier,steps, positionX, positionY, positionZ,
                directionX, directionY, directionZ,endDirectionX,endDirectionY,endDirectionZ, color);
            SelectedEnergy = energy as SpintronicsZeeman;
            Add(energy);
        }

        public void Add(IEnergy energy)
        {
            DataStorage.ObjectContainer.Add(energy);
            OnPropertyChanged(nameof(EnergiesNames));
        }
        public void AddPart(double directionX, double directionY,
            double directionZ,double endDirectionX, double endDirectionY, double endDirectionZ, IColor color, int steps)
        {
            _controller.AddEnergyPart(SelectedEnergy, EnergyType.UZeeman, directionX, directionY, directionZ, color,
                steps, null);
            _controller.AddEnergyPart(SelectedEnergy, EnergyType.UZeeman, directionX, directionY, directionZ, color,
                0, null);
        }
        public void DeleteEnergyPart( IEnergyPart energyPart)
        {
            var secondPart = SelectedEnergy.GetTwin(energyPart);
            _controller.DeleteEnergyPart(SelectedEnergy, energyPart);
            _controller.DeleteEnergyPart(SelectedEnergy, secondPart);//??
            OnPropertyChanged(nameof(EnergiesNames));
        }
        public void DeleteEnergyPart(int index)
        {
            DeleteEnergyPart( SelectedEnergy.GetEnergyPart(index));
        }

        public void UpdateEnergyPart(string name, double multiplier, int index, double startVectorX, double startVectorY, double startVectorZ, double endVectorX, double endVectorY, double endVectorZ,
            double pointX, double pointY, double pointZ, int steps, IColor color, bool visible, bool visibleSecond)
        {
            SelectedEnergy.Change(name, multiplier, index, startVectorX, startVectorY, startVectorZ, endVectorX,endVectorY,
                endVectorZ, pointX, pointY,pointZ,steps,color, visible,false);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private List<string> GetAtlases()
        {
            var list = new List<string>(DataStorage.ObjectContainer.MultiAtlases);
            list.AddRange(DataStorage.ObjectContainer.ObjectNames);
            return list;
        }

    }
}

using CoreLibrary;
using CoreLibrary.Enums;
using CoreLibrary.Factories;
using CoreLibrary.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace GUIProject.Models
{
    public class AnisotropyModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        EnergyPartController _controller = new EnergyPartController();
        AnisotropyFactory _factory = new AnisotropyFactory();
        public SpintronicsAnisotropy SelectedEnergy { get; set; }
        public List<string> Atlases => GetAtlases();

        public List<string> Regions => DataStorage.ObjectContainer.Regions;
        public List<string> EnergiesNames => DataStorage.ObjectContainer.EnergieNames;
        public IEnergy GetEnergy(string name)
        {
            return DataStorage.ObjectContainer.GetEnergy(name);
        }

        public void Add(string name,string region,string atlas,double value ,double positionX, double positionY,
            double positionZ,double directionX, double directionY, double directionZ,IColor color)
        {
            var atlasObject = DataStorage.ObjectContainer.GetObject(atlas);
            var energy = _factory.Create(name, region, atlasObject, value, positionX, positionY, positionZ, 
                directionX, directionY, directionZ, color);
            Add(energy);
        }

        public void Add(IEnergy energy)
        {
            DataStorage.ObjectContainer.Add(energy);
            OnPropertyChanged(nameof(EnergiesNames));
        }
        public void AddEnergyPart(double directionX, double directionY,
            double directionZ, IColor color, double value, string region)
        {
            _controller.AddEnergyPart(SelectedEnergy, EnergyType.UniaxialAnisotropy, directionX, directionY, directionZ, color,
                value, region);
        }
        public void DeleteEnergyPart(IEnergy energy, IEnergyPart energyPart)
        {
            _controller.DeleteEnergyPart(energy, energyPart);//??
            OnPropertyChanged(nameof(EnergiesNames));
        }
        public void DeleteEnergyPart(int index)
        {
            DeleteEnergyPart(SelectedEnergy,SelectedEnergy.GetEnergyPart(index));
        }

        public void UpdateEnergyPart(int index,string name, string atlas,string region,double value,double directionX, 
            double directionY, double directionZ ,double positionX, double positionY, double positionZ,IColor color,
            bool visible)
        {
            SelectedEnergy.Change(index, name, atlas, region, value, directionX, directionY, directionZ, positionX, 
                positionY, positionZ, color, visible);
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

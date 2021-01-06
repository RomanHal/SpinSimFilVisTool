using CoreLibrary;
using CoreLibrary.Enums;
using CoreLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GUIProject.Models
{
    public class EnergyModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public List<string> EnergiesNames => DataStorage.ObjectContainer.EnergieNames;
        public IEnergy GetEnergy(string name)
        {
            return DataStorage.ObjectContainer.GetEnergy(name);
        }


        public void Delete(string energyName)
        {
            Delete(GetEnergy(energyName));
        }
        public void Delete(IEnergy energy)
        {
            if (energy != null)
            {
                DataStorage.ObjectContainer.Delete(energy);
                OnPropertyChanged(nameof(EnergiesNames));
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

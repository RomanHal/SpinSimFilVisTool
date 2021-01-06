using CoreLibrary.Enums;
using CoreLibrary.Interfaces;
using GraphicLibrary.Management;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary
{
    public class EnergyPartController
    {
        EnergyPartCreator _creator = new EnergyPartCreator();

        public void AddEnergyPart(IEnergy energy,EnergyType type,double directionX, double directionY
            , double directionZ,IColor color,double value,string region)
        {
            var energyPart= _creator.Create(type, directionX, directionY, directionZ, color, value
                , region);
            AddEnergyPart(energy, energyPart);
        }
        public void AddEnergyPart(IEnergy energy,IEnergyPart energyPart)
        {
            FigureContainer.Add(((EnergyPart)energyPart)._graphicsObject);
            ((SpintronicsEnergy)energy).AddEnergyPart(energyPart);
        }
        public void DeleteEnergyPart(IEnergy energy,IEnergyPart energyPart)
        {
            FigureContainer.Remove(((EnergyPart)energyPart)._graphicsObject);
            ((SpintronicsEnergy)energy).RemoveEnergyPart(energyPart);
        }
    }
}

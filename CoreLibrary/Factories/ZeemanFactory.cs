using CoreLibrary.Enums;
using CoreLibrary.Interfaces;
using MifParser.Interfaces;
using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Factories
{
    public class ZeemanFactory
    {
        MifParser.Factories.ZeemanFactory _factory = new MifParser.Factories.ZeemanFactory();
        EnergyPartCreator _creator = new EnergyPartCreator();
        public IEnergy Create(string name, double multiplier, int steps, double positionX, double positionY,
            double positionZ, double directionX, double directionY, double directionZ, double endDirectionX, 
            double endDirectionY,  double endDirectionZ, IColor color)
        {
            var direction = new Vector(directionX, directionY, directionZ);
            var endDirection = new Vector(endDirectionX, endDirectionY, endDirectionZ);
            var mifEnergy = _factory.Create(name,multiplier,new Vector[] {direction },new Vector[] { endDirection},
                new int[] {steps });
            var energyPart = _creator.Create(EnergyType.UZeeman, directionX, directionY, directionZ,
                color, steps, null);
            var energyPart2 = _creator.Create(EnergyType.UZeeman, endDirectionX, endDirectionY, endDirectionZ, color, 0, null);
            return new SpintronicsZeeman(mifEnergy, new List<(IEnergyPart,IEnergyPart)>() { (energyPart,energyPart2) });
        }
    }
}

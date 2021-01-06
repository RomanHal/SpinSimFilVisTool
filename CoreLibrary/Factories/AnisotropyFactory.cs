using CoreLibrary.Enums;
using CoreLibrary.Interfaces;
using MifParser.Interfaces;
using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Factories
{
    public class AnisotropyFactory
    {
        MifParser.Factories.AnisotropyFactory _factory = new MifParser.Factories.AnisotropyFactory();
        EnergyPartCreator _creator = new EnergyPartCreator();
        public IEnergy Create(string name, string region, IObject atlas, double value, double positionX, double positionY,
            double positionZ, double directionX, double directionY, double directionZ, IColor color)
        {
            IMifAtlas mifAtlas =atlas!=null?((SpintronicsObject)atlas).MifObject:null;
            var mifEnergy = _factory.Create(name, value, new Vector(directionX, directionY, directionZ), null,
                mifAtlas, new List<(string, Vector, double)>());
            var energyPart = _creator.Create(EnergyType.UniaxialAnisotropy, directionX, directionY, directionZ,
                color, value, region);
            return new SpintronicsAnisotropy(mifEnergy, new List<IEnergyPart>() { energyPart });
        }
    }
}

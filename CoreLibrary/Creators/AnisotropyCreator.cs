using CoreLibrary.Interfaces;
using CoreLibrary.Positioners;
using MifParser.Energies;
using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary
{
    class AnisotropyCreator
    {
        EnergyPartCreator _creator = new EnergyPartCreator();
        public IEnergy CreateAnisotropy(IMifEnergy energy,IColor color)
        {
            switch(energy.GetType())
            {
                case Type T when T ==typeof(UniaxialAnisotropy):
                    return CreateUniaxialAnisotropy(energy, color);
                default: throw new NotImplementedException();
            }
            
        }
        private IEnergy CreateUniaxialAnisotropy(IMifEnergy energy,IColor color)
        {
            var anisotropy = energy as UniaxialAnisotropy;
            int energiesAmount = 0;
            if ((energy as UniaxialAnisotropy).Axis.Atlas != null)
                energiesAmount = (energy as UniaxialAnisotropy).Axis.RegionsValue.Count;
            List<IEnergyPart> energies = new List<IEnergyPart>();
            energies.Add(_creator.Create(Enums.EnergyType.UniaxialAnisotropy,
                anisotropy.Axis.DefaultValue.X, anisotropy.Axis.DefaultValue.Y,
                anisotropy.Axis.DefaultValue.Z, color, anisotropy.K1.DefaultValue, "default"));
            for (int i = 0; i < energiesAmount; i++)
            {
                var energyPart = _creator.Create(Enums.EnergyType.UniaxialAnisotropy,
                    anisotropy.Axis.RegionsValue[i].Item2.X, anisotropy.Axis.RegionsValue[i].Item2.Y,
                    anisotropy.Axis.RegionsValue[i].Item2.Z, color, anisotropy.K1.RegionsValue[i].Item2,
                    anisotropy.K1.RegionsValue[i].Item1);
                energies.Add(energyPart);
            }
            if(energiesAmount!=0)
            {
              //  Positioning(anisotropy, energies);
            }
            //else : Get max of all atlases x,y,z and select it as point<-how can I get atlases from here?

            return new SpintronicsAnisotropy(energy, energies);
        }

        private void Positioning(UniaxialAnisotropy anisotropy, List<IEnergyPart> energies)
        {
            foreach (var energyPart in energies)
            {
                energyPart.PositionX = anisotropy.Axis.Atlas.X.Max * XyzConstrainsModifier.Norm;
                energyPart.PositionY = anisotropy.Axis.Atlas.Y.Max * XyzConstrainsModifier.Norm;
                energyPart.PositionZ = anisotropy.Axis.Atlas.Z.Max * XyzConstrainsModifier.Norm;
            }
        }
    }
}

using CoreLibrary.GraphicsServices;
using CoreLibrary.Interfaces;
using MifParser.Energies;
using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary
{ 
    class EnergyCreator
    {
        GraphicCreator _graphicCreator = new GraphicCreator();
        AnisotropyCreator _anisotropyCreator = new AnisotropyCreator();
        ZeemanCreator _zeemanCreator = new ZeemanCreator();

        public IEnergy CreateEnergy(IMifEnergy energy,IColor color)
        {
            switch(energy.GetType())
            {
                case Type T when T == typeof(UniaxialAnisotropy):
                    return CreateAnisotropy(energy, color);
                case Type T when T == typeof(UZeeman):
                    return CreateZeeman(energy, color);
                default: throw new NotImplementedException();
            }
        }

        private IEnergy CreateZeeman(IMifEnergy energy, IColor color)
        {
           return _zeemanCreator.CreateZeeman(energy, color);
        }

        private IEnergy CreateAnisotropy(IMifEnergy energy, IColor color)
        {
            return _anisotropyCreator.CreateAnisotropy(energy, color);
        }
    }
}

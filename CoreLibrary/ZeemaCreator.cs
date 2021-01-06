using CoreLibrary.Interfaces;
using MifParser.Energies;
using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary
{
    class ZeemanCreator
    {
        EnergyPartCreator _creator = new EnergyPartCreator();

        public IEnergy CreateZeeman(IMifEnergy energy,IColor color)
        {
            switch(energy.GetType())
            {
                case Type T when T == typeof(UZeeman):
                    return CreateUZeeman(energy, color);

                default: throw new NotImplementedException();
            }
        }

        private IEnergy CreateUZeeman(IMifEnergy energy, IColor color)
        {
            var zeeman = energy as UZeeman;
            List<(IEnergyPart, IEnergyPart)> energies = new List<(IEnergyPart, IEnergyPart)>();
            zeeman.PositionDestinationStepsList.ForEach(e=> 
            {
                var part1 = _creator.Create(Enums.EnergyType.UZeeman, e.Item1.X, e.Item1.Y, e.Item1.Z, color, e.Item3, null);
                var part2 = _creator.Create(Enums.EnergyType.UZeeman, e.Item2.X, e.Item2.Y, e.Item2.Z, color, 0, null);
                energies.Add((part1, part2));
            });
            

            return new SpintronicsZeeman(energy,energies);
        }
    }
}

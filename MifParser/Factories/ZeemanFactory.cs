using MifParser.Energies;
using MifParser.Interfaces;
using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MifParser.Factories
{
    public class ZeemanFactory
    {
        public IMifEnergy Create(string name,double multiplier,Vector[] positions,Vector[] destinations=null,
            int[] steps=null)
        {
            switch(destinations)
            {
                case null:return CreateFixedZeeman(name, multiplier, positions[0]);
                
                default: return CreateUZeeman(name, multiplier, positions, destinations, steps);
            }
        }

        private IMifEnergy CreateUZeeman(string name, double multiplier, Vector[] positions, Vector[] destinations, int[] steps)
        {
            List<(Vector, Vector, int)> positionDestinationStepsList = new List<(Vector, Vector, int)>();
            positionDestinationStepsList = positions.Zip(destinations, (p, d) => (p, d)).
                Zip(steps, (v, s) => (v.p, v.d, s)).ToList();
            //for(int i=0;i<positions.Length; i++)
            //{
            //    positionDestinationStepsList.Add((positions[i], destinations[i], steps[i]));
            //}
            return new UZeeman(name, multiplier, positionDestinationStepsList);
        }

        private IMifEnergy CreateFixedZeeman(string name, double multiplier, Vector vector)
        {
            throw new NotImplementedException();
        }
    }
}

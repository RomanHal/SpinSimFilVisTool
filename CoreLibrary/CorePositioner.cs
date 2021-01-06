using CoreLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary
{
    class CorePositioner
    {
        List<IObject> _objects;
        List<IEnergy> _energies;
        List<IEnergyPositioner> Positioners { get; }
        
        public CorePositioner(List<IObject> objects,List<IEnergy> energies,List<IEnergyPositioner> energyPositioners)
        {
            _objects = objects;
            _energies = energies;
            Positioners = energyPositioners;
        } 

        public void Refresh()
        {
            _objects.ForEach(o => o.Refresh());
            Positioners.ForEach(p => p.Refresh());
        }

        private void RefreshEnergies()
        {
            
        }
    }
}

using CoreLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.FileServices.Interfaces
{
    interface IConverter
    {
        //List<IScript> GetScripts();
        //List<IEnergy> GetEnergies();
        //List<IObject> GetObjects();

        IObjectContainer Container { get; }
        
        
    }
}

using CoreLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.FileServices.Interfaces
{
    public interface IFileObjectsContainer
    {
        List<IObject> Objects { get; set; }
        List<IEnergy> Energies { get; set; }
        List<IScript> Scripts { get; set; }
        List<List<string>> UnprocessedText { get; set; }

    }
}

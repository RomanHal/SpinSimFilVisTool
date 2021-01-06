using CoreLibrary.Enums;
using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Interfaces
{
    public interface IEnergy
    {
        string Name { get; set; }
        EnergyType Type { get; }
    }
}

using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Energies
{
    public abstract class Energy:IMifEnergy
    {
        public string Name { get; set; }
        
        public Energy(string name)
        {
            Name = name;
        }

        internal abstract string[] GetText();

    }
}

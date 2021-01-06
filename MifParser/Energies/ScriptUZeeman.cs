using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Energies
{
    class ScriptUZeeman : Energy
    {
        string ScriptArgs { get; set; }
        IMifScript Script { get; set; }
        double Multiplier { get; set; }

        public ScriptUZeeman(string name) : base(name)
        {
            throw new NotImplementedException();
        }

        internal override string[] GetText()
        {
            throw new NotImplementedException();
        }
    }
}

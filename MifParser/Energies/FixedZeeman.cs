using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Energies
{
    class FixedZeeman : Energy
    {
        IMifVectorField VectorField { get; set; }
        double Multiplier { get; set; }
        public FixedZeeman(string name):base(name)
        {
            throw new NotImplementedException();
        }

        internal override string[] GetText()
        {
            throw new NotImplementedException();
        }
    }
}

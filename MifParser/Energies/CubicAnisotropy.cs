using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Energies
{
    class CubicAnisotropy:Energy
    {
        IMifScalarField K1 { get; set; }
        IMifVectorField Axis1 { get; set; }
        IMifVectorField Axis2 { get; set; }

        public CubicAnisotropy(string name,IMifScalarField k1,IMifVectorField axis1,IMifVectorField axis2)
            :base(name)
        {
            K1 = k1;
            Axis1 = axis1;
            Axis2 = axis2;
        }

        internal override string[] GetText()
        {
            throw new NotImplementedException();
        }
    }
}

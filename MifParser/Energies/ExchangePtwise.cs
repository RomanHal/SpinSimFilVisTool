using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Energies
{
    public class ExchangePtwise : Energy
    {
        IMifScalarField ScalarField_Spec { get; set; }

        public ExchangePtwise(string name):base(name)
        {
            throw new NotImplementedException();
        }

        internal override string[] GetText()
        {
            throw new NotImplementedException();
        }
    }
}

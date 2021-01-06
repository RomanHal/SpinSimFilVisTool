using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Fields
{
    class ScalarFieldName : IMifScalarField
    {
        public string Name { get; set; }
        public IMifAtlas Atlas { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double DefaultValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<(string, double)> RegionsValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ScalarFieldName() { }
        public ScalarFieldName(string name)
        {
            Name = name;
        }
    }
}

using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Fields
{
    public class AtlasScalarField:IMifScalarField
    {
        public string Name { get; set; }
        public IMifAtlas Atlas { get; set; }
        public double DefaultValue { get; set; }

        public List<(string, double)> RegionsValue { get => _regionsValue; set => _regionsValue = value; }
        List<(string, double)> _regionsValue = new List<(string, double)>();

        public AtlasScalarField() { }
        public AtlasScalarField(IMifAtlas atlas,double defaultValue, List<(string, double)> values)
        {
            Atlas = atlas;
            DefaultValue = defaultValue;
            RegionsValue = values;
        }
    }
}

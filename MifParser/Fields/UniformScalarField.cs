using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Fields
{
    class UniformScalarField:IMifScalarField
    {
        public string Name { get; set; }
        public IMifAtlas Atlas { get => null; set { } }
        public double DefaultValue { get => Value; set => Value=value; }
        public List<(string, double)> RegionsValue { get => null; set { } }
        double Value { get; set; }
        public UniformScalarField(double value)
        {
            Value = value;
        }
    }
}

using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Fields
{
    class LinearScalarField:IMifScalarField
    {
        public string Name { get ; set ; }
        public IMifAtlas Atlas { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double DefaultValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<(string, double)> RegionsValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double X { get; set; }
        double Y { get; set; }
        double Z { get; set; }
        double Norm { get; set; }
    }
}

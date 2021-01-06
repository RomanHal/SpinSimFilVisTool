using MifParser.Interfaces;
using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Fields
{
    class UniformVectorField:IMifVectorField
    {
        public IMifAtlas Atlas { get => null; set { } }
        public Vector DefaultValue { get => Vector*Normal; set { Vector = value.Normalized();
                Normal = value.Length;
            }  }
        public List<(string, Vector)> RegionsValue { get => null; set  { } }
        Vector Vector { get; set; }
        double Normal { get; set; }
        public UniformVectorField(Vector vector)
        {
            DefaultValue = vector;
        }
        public UniformVectorField(Vector vector,double normal)
        {
            Vector = vector;
            Normal = normal;
        }
    }
}

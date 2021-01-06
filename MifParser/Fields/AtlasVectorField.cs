using MifParser.Interfaces;
using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Fields
{
    class AtlasVectorField:IMifVectorField
    {
        public IMifAtlas Atlas { get; set; }
        
        public Vector DefaultValue { get => _defaultValue; set => _defaultValue = value; }
        Vector _defaultValue = new Vector();
        public List<(string, Vector)> RegionsValue { get => _regionsValue; set => _regionsValue = value; }
        List<(string, Vector)> _regionsValue= new List<(string, Vector)>();
        public AtlasVectorField(IMifAtlas atlas,Vector defaultValue, List<(string, Vector)> regionsValues )
        {
            Atlas = atlas;
            _defaultValue = defaultValue;
            RegionsValue = regionsValues;
        }
    }
}

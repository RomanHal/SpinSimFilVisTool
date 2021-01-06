using MifParser.Interfaces;
using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Atlases
{
    class AtlasNameOnly : IMifAtlas
    {
        public string Name { get ; set ; }

        public AtlasNameOnly(string name)
        {
            Name = name;
        }
        public Range X { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Range Y { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Range Z { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<string> Regions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

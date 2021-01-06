using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Energies
{
    public class Exchange6Ngbr : Energy
    {
        double DefaultA { get; set; }
        IMifAtlas atlas;
        List<(string, string, double)> RegionRegionValue;
        public Exchange6Ngbr(string name,double defaultA,IMifAtlas atlas):base(name)
        {

        }

        internal override string[] GetText()
        {
            throw new NotImplementedException();
        }
    }
}

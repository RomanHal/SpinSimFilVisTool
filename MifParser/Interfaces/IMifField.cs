using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Interfaces
{
    public interface IMifScalarField
    {
        string Name { get; set; }
        IMifAtlas Atlas { get; set; }
        double DefaultValue { get; set; }
        List<(string,double)> RegionsValue { get; set; }
    }
}

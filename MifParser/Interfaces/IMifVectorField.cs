using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Interfaces
{
    public interface IMifVectorField
    {
        IMifAtlas Atlas { get; set; }
        Vector DefaultValue { get; set; }
        List<(string,Vector)> RegionsValue { get; set; }
    }
}

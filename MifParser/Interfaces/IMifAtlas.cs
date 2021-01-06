using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Interfaces
{
    public interface IMifAtlas
    {
        string Name { get; set; }
        Range X { get; set; }
        Range Y { get; set; }
        Range Z { get; set; }
        List<string> Regions { get; set; }

    }
}

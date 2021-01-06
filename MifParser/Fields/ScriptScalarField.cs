using MifParser.Interfaces;
using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Fields
{
    class ScriptScalarField
    {
        IMifScript script;
        string scriptArgs;
        IMifAtlas atlas;
        Range X { get; set; }
        Range Y { get; set; }
        Range Z { get; set; }
    }
}

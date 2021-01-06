using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Interfaces
{
    public interface IMifScript
    {
        string Name { get; set; }
        string Script { get; set; }
        string Args { get; set; }
    }
}

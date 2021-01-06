using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Scripts
{
    class ScriptNameOnly : Interfaces.IMifScript
    {
        public string Name { get; set; }
        public string Script { get => null; set =>Script=null; }
        public string Args { get ; set ; }

        public ScriptNameOnly(string name)
        {
            Name = name;
        }
    }
}

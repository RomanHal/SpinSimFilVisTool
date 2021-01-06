using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Scripts
{
    public class ScriptObject:Interfaces.IMifScript
    {
        public string Name { get ; set ; }
        public string Script { get; set; }
        public string Args { get; set; }

        public ScriptObject() { }
        public ScriptObject(string name,string script,string args)
        {
            Name = name;
            Script = script;
            Args = args;
        }
        internal string[] GetText()
        {
            List<string> lines = new List<string>
            {
                "proc " + Name + " { " + Args + " } {"
            };
            var scriptText= Script.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            lines.AddRange(scriptText);
            lines.Add("}");
            return lines.ToArray();
        }

    }
}

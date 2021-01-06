using MifParser.Interfaces;
using MifParser.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MifParser.Factories
{
    class ScriptFactoryText
    {
        public IMifScript CreateScript(string[] lines)
        {
            var name =string.Concat(lines[0].Select((c, i) => new { c, i })
                .Where(s => s.i > lines[0].IndexOf("proc")+3 && s.i < lines[0].IndexOf('{')).Select(s=>s.c).Where(c=>c!=' '));
            var args = string.Concat(lines[0].Select((c, i) => new { c, i })
                .Where(s => s.i > lines[0].IndexOf('{') && s.i < lines[0].IndexOf('}')).Select(s => s.c));
            var script = string.Join(Environment.NewLine,lines.Skip(1).Take(lines.Length - 2));
            return new ScriptObject(name, script, args);
        }
    }
}

using MifParser.Interfaces;
using MifParser.Parts;
using System.Collections.Generic;
using System.Linq;

namespace MifParser.Atlases
{
    public class ScriptAtlas :Atlas
    {
        public override List<string> Regions { get; set; }
        public string ScriptArgs { get; set; }
        public IMifScript Script { get; set; }

        public ScriptAtlas(string name, Range x, Range y, Range z, List<string> regions, 
            string srciptArgs, IMifScript script)
            :base(name,x,y,z)
        {
            Regions = regions;
            ScriptArgs = srciptArgs;
            Script = script;
        }

        public ScriptAtlas(string name,double xMax, double xMin, double yMax, double yMin, double zMax,
            double zMin, List<string> regions, string srciptArgs, IMifScript script) 
            : base(name, xMax, xMin, yMax, yMin, zMax, zMin)
        {
            Regions = regions;
            ScriptArgs = srciptArgs;
            Script = script;
        }

        internal override string[] GetText()
        {
            return new string[] {
            "Oxs_ScriptAtlas:"+Name+" {",
            "xrange { "+X.Min+" "+X.Max+" }",
            "yrange { "+Y.Min+" "+Y.Max+" }",
            "zrange { "+Z.Min+" "+Z.Max+" }",
            "regions { "+string.Join(" ",Regions)+" }",
            "script_args { "+ScriptArgs+" }",
            "script "+Script.Name,
            "}"};
        }
    }
}

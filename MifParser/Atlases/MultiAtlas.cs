using MifParser.Interfaces;
using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MifParser.Atlases
{
    public class MultiAtlas:Atlas
    {
        public List<IMifAtlas> MifAtlas { get; set; }

        public override List<string> Regions { get => MifAtlas.SelectMany(m=>m.Regions).ToList(); set => throw new NotImplementedException(); }

        public MultiAtlas(string name, List<IMifAtlas> mifAtlas):base(name,0,0,0,0,0,0)
        {
            MifAtlas = mifAtlas;
        }

        internal override string[] GetText()
        {
            List<string> lines = new List<string>
            {
                "Oxs_MultiAtlas:" + Name + " [subst {",
                ""
            };
            MifAtlas.ForEach(a => {
                var atlas = ((Atlas)a).GetText();
                atlas[0] = "atlas { " + atlas[0];
                atlas[atlas.Length - 1] = atlas[atlas.Length - 1] + " }";
                lines.AddRange(atlas);
                lines.Add("");
            });
            lines.Add("");
            lines.Add("}]");
            return lines.ToArray();
        }
    }
}

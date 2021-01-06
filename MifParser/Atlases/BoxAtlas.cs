using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Atlases
{
    public class BoxAtlas:Atlas
    {
        public override List<string> Regions { get => new List<string>() {Region };
            set =>Region= (value!=null)?value[0]:Region=null; }
        public string Region { get; set; }
        public BoxAtlas(string name,double xMax, double xMin, double yMax, 
            double yMin, double zMax, double zMin,string region)
            :base(name, xMax, xMin, yMax, yMin, zMax, zMin)
        {
            Region = region;
        }

        public BoxAtlas(string name, Range x, Range y, Range z,string region)
            :base(name,x,y,z)
        {
            Region = region;
        }

        internal override string[] GetText()
        {
            return new string[] {
            "Oxs_BoxAtlas:"+Name+" {",
            "xrange {"+X.Min+" "+X.Max+"}",
            "yrange {"+Y.Min+" "+Y.Max+"}",
            "zrange {"+Z.Min+" "+Z.Max+"}",
            "region "+Region,
            "}"};
        }
    }
}

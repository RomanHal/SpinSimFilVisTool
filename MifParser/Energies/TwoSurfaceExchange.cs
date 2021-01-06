using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Energies
{
    public class TwoSurfaceExchange:Energy
    {
        public double Sigma { get; set; }
        public double Sigma2 { get; set; }
        public Surface Surface1 { get; set; }
        public Surface Surface2 { get; set; }

        public TwoSurfaceExchange():base(null) { }
        public TwoSurfaceExchange(string name, double sigma, double sigma2,Surface surface,Surface surface2):base(name)
        {
            Sigma = sigma;
            Sigma2 = sigma2;
            Surface1 = surface;
            Surface2 = surface2;
        }

        internal override string[] GetText()
        {
            List<string> list = new List<string>();
            list.AddRange(new string[] { "Oxs_TwoSurfaceExchange:" + Name + " {","sigma "+Sigma,"sigma2 "
                +Sigma2+"surface1 {" });
            list.AddRange(Surface1.GetText());
            list.Add("}");
            list.Add("surface2 {");
            list.AddRange(Surface2.GetText());
            list.Add("}");
            list.Add("}");
            return list.ToArray();
        }
    }
}

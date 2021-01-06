using System;
using System.Collections.Generic;
using System.Text;
using MifParser.Interfaces;

namespace MifParser.Energies
{
    public class UniaxialAnisotropy:Energy
    {
        public IMifScalarField K1 { get; set; }
        public IMifVectorField Axis { get; set; }

        public UniaxialAnisotropy():base(null) { }
        public UniaxialAnisotropy(string name,IMifScalarField k1,IMifVectorField axis):base(name)
        {
            K1 = k1;
            Axis = axis;
        }

        internal override string[] GetText()
        {
            List<string> list = new List<string>();
            list.Add("Specify Oxs_UniaxialAnisotropy:" + Name + " [subst { ");
            if (K1.Atlas != null)
            {
                list.AddRange(new string[] { "K1 { Oxs_AtlasScalarField {","atlas :"+K1.Atlas.Name,
                    "default_value " +K1.DefaultValue, "values {" });
                foreach (var value in K1.RegionsValue)
                {
                    list.Add(value.Item1 + " " + value.Item2);
                }
                list.AddRange(new string[] {"}","}}", "axis { Oxs_AtlasVectorField {", "atlas :"+Axis.Atlas.Name,
                "default_value { "+Axis.DefaultValue.ToString()+" }","values {"});
                foreach (var value in Axis.RegionsValue)
                {
                    list.Add(value.Item1 + " { " + value.Item2.ToString()+" }");
                }
                list.AddRange(new string[] { "}", "}}", "} ]" });
            }



            return list.ToArray();
        }
    }
}

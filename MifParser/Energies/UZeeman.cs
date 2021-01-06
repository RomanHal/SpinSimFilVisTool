using MifParser.Interfaces;
using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Energies
{
    public class UZeeman:Energy
    {
        public double Multiplier { get; set; }
        public List<(Vector,Vector, int)> PositionDestinationStepsList { get; set; }//Hrange

        public UZeeman(string name,double multiplier,List<(Vector,Vector,int)> positionDestinationStepsList) :base(name)
        {
            Multiplier = multiplier;
            PositionDestinationStepsList = positionDestinationStepsList;
        }
        internal override string[] GetText()
        {
            List<string> list = new List<string>();
            list.AddRange(new string[] { "Oxs_UZeeman {", "multiplier " + Multiplier, "Hrange {" });
            foreach(var value in PositionDestinationStepsList)
            {
                list.Add("{ " + value.Item1.ToString() + " " + value.Item2.ToString() + " " + value.Item3 + " }");
            }
            list.Add("}");
            list.Add("}");
            return list.ToArray();
        }
    }
}

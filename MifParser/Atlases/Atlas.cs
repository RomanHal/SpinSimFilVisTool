using MifParser.Interfaces;
using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MifParser.Atlases
{
    public abstract class Atlas : IMifAtlas
    {
        public string Name { get; set; }
        public virtual Range X { get; set; }
        public virtual Range Y { get; set; }
        public virtual Range Z { get; set; }
        public abstract List<string> Regions { get; set; } 

        public Atlas() { }
        public Atlas(string name,double xMax,double xMin,double yMax,double yMin, double zMax, double zMin)
        {
            Name = name;
            X = new Range(xMin, xMax);
            Y = new Range(yMin, yMax);
            Z = new Range(zMin, zMax);
        }
        public Atlas (string name,Range x,Range y, Range z)
        {
            Name = name;
            X = x;
            Y = y;
            Z = z;
        }

        internal abstract string[] GetText();

    }
}

using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicLibrary.Interfaces
{
    public interface IGraphicsObject
    {
        Color Color{get;set;}
        bool Visible { get; set; }
        FigureType Type { get; set; }
        string Id { get; set; }
        Point FirstPoint { get; set; }
        Point LastPoint { get; set; }
        double D1 { get; set; }
        double D2 { get; set; }



    }
}

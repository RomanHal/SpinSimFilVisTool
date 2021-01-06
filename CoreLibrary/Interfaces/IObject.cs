using CoreLibrary.Enums;
using System.Collections.Generic;

namespace CoreLibrary.Interfaces
{
    public interface IObject
    {
        string Name { get; set; }
        double Xmin { get; set; }
        double Xmax { get; set; }
        double Ymin { get; set; }
        double Ymax { get; set; }
        double Zmin { get; set; }
        double Zmax { get; set; }
        string Multiatlas { get; set; }
        bool ColorByMs { get; set; }
        IColor Color { get; set; }
        bool Visible { get; set; }
        AtlasType Type { get; }
        List<string> Regions { get; set; }
        IScript Script { get; set; }
        string ScriptArgs { get; set; }
        void Refresh();
    }
}

using Avalonia.Media;
using CoreLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GUIProject.Other
{
    class GuiColor : IColor
    {
        public GuiColor(byte r,byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
        public GuiColor(Color color) : this(color.R, color.G, color.B) { }
        public byte R { get ; set ; }
        public byte G { get; set; }
        public byte B { get; set; }
    }
}

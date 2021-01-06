using CoreLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.GraphicsServices
{
    public struct CoreColor : IColor
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public CoreColor(OpenTK.Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
        }
    }
}

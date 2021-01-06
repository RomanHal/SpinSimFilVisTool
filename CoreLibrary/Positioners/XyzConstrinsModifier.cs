using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Positioners
{
    public static class XyzConstrainsModifier
    {
        public static double XConstrin { get ; set ; }
        public static double YConstrin { get ; set ; }
        public static double ZConstrin { get ; set ; }
        internal static double Norm { get; set; }
        static XyzConstrainsModifier()
        {
            XConstrin = 1;
            YConstrin = 1;
            ZConstrin = 1;
            Norm = 1;
        }
    }
}

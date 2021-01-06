using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Parts
{
    public class Range
    {
        public double Min { get; set; }
        public double Max { get; set; }
        public double Length { get => Math.Abs(Max - Min); }
        public Range(double min, double max)
        {
            Min = min;
            Max = max;
        }
        public static Range operator *(Range range, double multiplier)
        {
            return new Range(range.Max * multiplier, range.Min * multiplier);
        }
    }
}

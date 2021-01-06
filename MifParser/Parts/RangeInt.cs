using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Parts
{
    struct RangeInt
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public RangeInt(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Interfaces
{
    interface IEnergyPositioner
    {
        void Refresh();
        void ApplyDefaultPosition();
    }
}

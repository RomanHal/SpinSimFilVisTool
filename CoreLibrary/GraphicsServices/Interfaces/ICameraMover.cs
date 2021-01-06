using CoreLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.GraphicsServices.Interfaces
{
    public interface ICameraMover
    {
        void LookAt(IObject @object);
        void LookAt(IEnergy energy,int index);
    }
}

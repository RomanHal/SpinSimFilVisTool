using CoreLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.FileServices.Interfaces
{
    public interface IFileSaver
    {
        void Save(IObjectContainer container, string patch);
    }
}

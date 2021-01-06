using CoreLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.FileServices.Interfaces
{
    interface IFileLoader
    {
        IObjectContainer LoadFile(string patch);
    }
}

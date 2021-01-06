using GraphicLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Interfaces
{
    interface IGraphicsLibraryManagement
    {
        void CloseWindow();
        void PrintScreen(string patch);
        string GetSelectedObjectId();
    }
}

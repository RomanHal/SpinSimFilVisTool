using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Interfaces
{
    interface IGraphicsWindow
    {
        void OpenWindow();
        void CloseWindow();
        void AddElement(string Id);
        void DeleteElement(string Id);
        void AddElements(string[] IdList);
        void DeleteElements(string[] IdList);
        string GetSelectedObjectId();

    }
}

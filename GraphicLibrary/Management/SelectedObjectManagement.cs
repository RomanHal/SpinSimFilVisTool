using GraphicLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicLibrary.Management
{
    public static class SelectedObjectManagement
    {
        public delegate void OnClickSelect(IGraphicsObject graphicsObject);
        public static OnClickSelect OnClickSelectMethod;
    }
}

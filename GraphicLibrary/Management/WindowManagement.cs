using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicLibrary.Management
{
    public static class WindowManagement
    {
        public static CloseWindowEvent closeWindow = new CloseWindowEvent();
        public static MoveWindowEvent moveWindow = new MoveWindowEvent();
    }
}

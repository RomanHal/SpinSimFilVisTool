using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicLibrary.Configuration
{
    class MouseController
    {
        readonly MouseDevice _mouse;
        EventHandler<MouseButtonEventArgs> mouseDown;
        public MouseController(MouseDevice mouse)
        {
            _mouse = mouse;
          //  mouseDown = mouse.ButtonDown;
        }

        public Vector3 getMouseInWorldCoords()
        {
            throw new NotImplementedException();
        }
    }
}

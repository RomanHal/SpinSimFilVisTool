using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicLibrary.Configuration
{
    class Light
    {
        public void SetLights()
        {
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Light(LightName.Light0, LightParameter.Diffuse, 0.0001f);
            GL.Light(LightName.Light0, LightParameter.Ambient, 0.0001f);
        }
    }
}

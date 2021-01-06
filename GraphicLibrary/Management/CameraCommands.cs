using GraphicLibrary.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicLibrary.Management
{
    public class CameraCommands
    {
        public void MoveTo(double x,double y,double z)
        {
            MoveToCommand?.Invoke(this, new CameraCommandsEventArgs(x, y, z));
        }
        public void MoveBy(double x, double y, double z)
        {
            MoveByCommand?.Invoke(this, new CameraCommandsEventArgs(x, y, z));
        }

        internal event EventHandler<CameraCommandsEventArgs> MoveToCommand;
        internal event EventHandler<CameraCommandsEventArgs> MoveByCommand;
    }

    public class CameraCommandsEventArgs:EventArgs
    {
        public CameraCommandsEventArgs(double x,double y,double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}

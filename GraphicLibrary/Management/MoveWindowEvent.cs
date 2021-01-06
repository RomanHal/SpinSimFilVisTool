using System;


namespace GraphicLibrary.Management
{
    public class MoveWindowEvent
    {
        public void MoveWindow(int x, int y)
        {
            OnWindowsMove(new MoveWindowEventArgs() { X = x, Y = y });
        }
        protected virtual void OnWindowsMove(MoveWindowEventArgs e)
        {
            MoveWindowsCommand?.Invoke(this, e);
        }

        internal event EventHandler<MoveWindowEventArgs> MoveWindowsCommand;
    }

    public class MoveWindowEventArgs:EventArgs
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}

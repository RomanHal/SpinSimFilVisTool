using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicLibrary.Management
{
    public class CloseWindowEvent
    {

        public void CloseWindow()
        {
            OnCloseWindow(EventArgs.Empty);
        }
        protected virtual void OnCloseWindow(EventArgs e)
        {
            CloseWindowCommand?.Invoke(this, e);
        }

        internal event EventHandler<EventArgs> CloseWindowCommand;
    }
}

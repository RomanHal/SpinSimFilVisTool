using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicLibrary.Management
{
    public class ReturnFocusEvent
    {
        internal void ReturnFocus()
        {
            OnReturnFocus(EventArgs.Empty);
        }

        protected virtual void OnReturnFocus(EventArgs e)
        {
            ReturningFocus?.Invoke(this, e);
        }


        public event EventHandler<EventArgs> ReturningFocus;
    }
}

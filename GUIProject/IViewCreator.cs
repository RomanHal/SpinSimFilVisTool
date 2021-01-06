using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace GUIProject
{
    interface IViewCreator
    {
        IControl Build(object data, params object[] args);

        IControl Build(object data);
    }
}

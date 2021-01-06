using System.Collections.Generic;

namespace CoreLibrary.Interfaces
{
    public interface IScript
    {
        string Text { get; set; }
        string Args { get; set; }
        string Name { get; set; }
    }
}
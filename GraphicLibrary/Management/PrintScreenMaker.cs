using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicLibrary.Management
{
    public static class PrintScreenMaker
    {
        public static void PrintScreen(string path)
        {
            ExecutePrintScreen(new PrintScreenEventArgs() { Path = path });
        }

        static void ExecutePrintScreen(PrintScreenEventArgs e)
        {
            PrintScreenCommand?.Invoke(null, e);
        }

        internal static event EventHandler<PrintScreenEventArgs> PrintScreenCommand;
    }

    public class PrintScreenEventArgs:EventArgs
    {
        public string Path { get; set; }
    }
}

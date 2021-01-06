using CoreLibrary.GraphicsServices.Interfaces;
using GraphicLibrary.Management;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.GraphicsServices
{
    public class PrintScreener : IPrintScreenMaker
    {
        const string dateTimeFormat = "yyyyMMddTHHmmssffff";
        public PrintScreener()
        {
            
        }

        public void PrintScreen(string patch)
        {
            patch =  (patch[patch.Length-1] == '/' || patch[patch.Length - 1] == '\\') ? patch : patch + @"\";
            PrintScreenMaker.PrintScreen(patch + DateTime.Now.ToString(dateTimeFormat));
        }
    }
}

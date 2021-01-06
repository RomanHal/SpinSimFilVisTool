using CoreLibrary.FileServices.Interfaces;
using CoreLibrary.Interfaces;
using MifParser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CoreLibrary.FileServices
{
    public class FileLoader : IFileLoader
    {
        FileReader _reader= new FileReader();
        public IObjectContainer LoadFile(string patch)
        {
        CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            var converter = new Converter(_reader.ReadFile(patch));
            converter.Convert();
            return converter.Container;
        }
    }
}

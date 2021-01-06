using MifParser.Utility;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace MifParser
{
    class FileLoader
    {
        TextProcessingHelper _helper = new TextProcessingHelper();
        public string[] LoadFile(string patch)
        {
            var lines = File.ReadAllLines(patch);
            lines = _helper.RemoveFromText(lines, "\t");
            return lines;
        }
        
        
        
    }
}

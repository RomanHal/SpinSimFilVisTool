using CoreLibrary.FileServices.Interfaces;
using CoreLibrary.Interfaces;
using MifParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.FileServices
{
    public class FileSaver : IFileSaver
    {
        FileWriter _fileWriter = new FileWriter();
        public void Save(IObjectContainer container, string patch)
        {
            if (container is ObjectsContainer)
                _fileWriter.SaveFile(patch, ((ObjectsContainer)container).GetContainer());
            else throw new NotSupportedException();
        }
        public void Save(string[] text, string patch)
        {
            _fileWriter.SaveFile(patch, text);
        }

        public string[] Preview(IObjectContainer container)
        {
            if (container is ObjectsContainer)
                return _fileWriter.PreView(((ObjectsContainer)container).GetContainer());
            else throw new NotSupportedException();
        }
    }
}

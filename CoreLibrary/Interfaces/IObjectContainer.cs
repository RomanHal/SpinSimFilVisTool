using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.Events;

namespace CoreLibrary.Interfaces
{
    public interface IObjectContainer
    {
        List<string> MultiAtlases { get; }
        List<string> ObjectNames { get; }
        IObject GetObject(string name);
        void Delete(IObject @object);
        void Add(IObject @object);
        List<string> EnergieNames { get; }
        IEnergy GetEnergy(string name);
        void Delete(IEnergy energy);
        void Add(IEnergy energy);
        List<string> ScriptsNames { get; }
        IScript GetScript(string name);
        void Delete(IScript script);
        void Add(IScript script);
        List<List<string>> UnprocessedText { get; set; }
        List<string> Regions { get; }
        void Clear();
        OnClickSelect OnClickSelectMethod { set; }
    }
}

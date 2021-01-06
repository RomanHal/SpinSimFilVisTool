using CoreLibrary.Interfaces;
using MifParser.Interfaces;
using MifParser.Scripts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary
{
    public class MifScript : ScriptObject, IScript
    {
        public MifScript(string name,string text,string args):base(name,text,args)
        {
        }
        public MifScript() { }
        public MifScript (IMifScript scriptObject)
        {
            Name = scriptObject.Name;
            Args = scriptObject.Args;
            Script = scriptObject.Script;
        }
        public string Text { get => Script; set => Script = value; }
        internal ScriptObject GetScriptObject()
        {
            return this;
        }

    }
}

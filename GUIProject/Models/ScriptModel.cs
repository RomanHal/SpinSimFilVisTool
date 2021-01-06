using CoreLibrary;
using CoreLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GUIProject.Models
{
    public class ScriptModel: INotifyPropertyChanged
    {
        public List<string> Scripts { get => DataStorage.ObjectContainer.ScriptsNames; }

        public event PropertyChangedEventHandler PropertyChanged;

        public IScript GetScript(string name)
        {
            return DataStorage.ObjectContainer.GetScript(name);
        }
        

        public void Add(string name,string text,string args)
        {
            Add(new MifScript(name, text, args));
        }
        public void Add(IScript script)
        {
            DataStorage.ObjectContainer.Add(script);
            OnPropertyChanged(nameof(Scripts));
        }
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using CoreLibrary.Interfaces;
using GUIProject.Models;
using GUIProject.Other;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GUIProject.ViewModels
{
    public class NewScriptViewModel
    {
        public GuiProperty<bool> IsNew { get => _isNew; set => _isNew = value; }
        public ObservableCollection<string> Scripts { get =>new ObservableCollection<string>(_model.Scripts); }
        public GuiProperty<string> Name { get => _name; set => _name = value; }       
        public GuiProperty<string> Args { get => _args; set => _args = value; }
        public GuiProperty<string> Text { get => _text; set => _text = value; }
        public Action Close { get; set; }

        GuiProperty<bool> _isNew = new GuiProperty<bool>();
        GuiProperty<string> _name = new GuiProperty<string>();
        GuiProperty<string> _args = new GuiProperty<string>();
        GuiProperty<string> _text = new GuiProperty<string>();
        ScriptModel _model = new ScriptModel();
        IScript _selectedScript;

        public void CreateNew()
        {
            IsNew.Value = true;
        }
        public void Save()
        {
            if(IsNew.Value)
            {
                _model.Add(Name.Value, Text.Value, Args.Value);
                IsNew.Value = false;
                _selectedScript = _model.GetScript(Name.Value);
            }
            else if(_selectedScript!=null)
            {
                _selectedScript.Args = Args.Value;
                _selectedScript.Name = Name.Value;
                _selectedScript.Text = Text.Value;
            }
        }
        
    }
}

using GUIProject.Models;
using GUIProject.Other;
using System;
using System.Collections.Generic;
using System.Text;

namespace GUIProject.ViewModels
{
    public class UnInterpretedTextViewModel
    {
        public GuiProperty<int> PartNumber { get => _partNumber;
            set => _partNumber = (value.Value>-1&&value.Value<4) ? value:_partNumber; }
        GuiProperty<int> _partNumber= new GuiProperty<int>();
        public GuiProperty<string> Text { get => _text; set => _text = value; }
        GuiProperty<string> _text=new GuiProperty<string>();
        GuiProperty<string> PartDescription { get => _partDescription;
            set => _partDescription = value; }
        GuiProperty<string> _partDescription=new GuiProperty<string>();
        private const string _description = " of 4";
        UnInterpretedTextModel _model = new UnInterpretedTextModel();
        public Action Close { get; set; }
        public UnInterpretedTextViewModel()
        {
            _partDescription.Value = (PartNumber.Value + 1).ToString() + _description;
            Text.Value = _model.GetTextPart(0);
            _partNumber.PropertyChanged += PartNumberValidation;
        }

        private void PartNumberValidation(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_partNumber.Value > 3) _partNumber.Value = 3;
            if (_partNumber.Value < 0) _partNumber.Value = 0;
        }

        public void Next()
        {
            _partNumber.Value++;
            GetTextData();

        }

        public void Previous()
        {
            _partNumber.Value--;
            GetTextData();
        }
        private void GetTextData()
        {
            _partDescription.Value = (PartNumber.Value + 1).ToString() + _description;
            Text.Value = _model.GetTextPart(_partNumber.Value);
        }

        public void Cancel()
        {
            Close.Invoke();
        }
        public void Apply()
        {
            _model.SetTextPart(_text.Value, _partNumber.Value);
        }
    }
}

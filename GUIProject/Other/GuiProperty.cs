using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GUIProject.Other
{
    public class GuiProperty<T> : INotifyPropertyChanged
    {
        T _data;

        public GuiProperty()
        {
        }
        public GuiProperty(T data)
        {
            _data = data;
        }
        public T Value { get => _data;set {
                _data = value;
                OnPropertyChanged(nameof(Value));
            } }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

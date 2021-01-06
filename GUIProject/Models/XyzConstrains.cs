using CoreLibrary.Positioners;
using GUIProject.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GUIProject.Models
{
    class XyzConstrains : INotifyPropertyChanged
    {
        // SpintronicsObject
        public event PropertyChangedEventHandler PropertyChanged;

        public GuiProperty<double> X { get => new GuiProperty<double>(XyzConstrainsModifier.XConstrin);
            set => XyzConstrainsModifier.XConstrin = value.Value; }
        public GuiProperty<double> Y
        {
            get => new GuiProperty<double>(XyzConstrainsModifier.YConstrin);
            set => XyzConstrainsModifier.YConstrin = value.Value;
        }
        public GuiProperty<double> Z
        {
            get => new GuiProperty<double>(XyzConstrainsModifier.ZConstrin);
            set => XyzConstrainsModifier.ZConstrin = value.Value;
        }
        public void IncreaseX(double amount)
        {
            X=new GuiProperty<double> (amount + X.Value);
        }
        public void IncreaseY(double amount)
        {
            Y=new GuiProperty<double> (amount + Y.Value);
        }
        public void IncreaseZ(double amount)
        {
            Z= new GuiProperty<double>(amount + Z.Value);
        }

    }
}

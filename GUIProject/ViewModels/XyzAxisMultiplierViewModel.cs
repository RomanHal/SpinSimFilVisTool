using CoreLibrary.Positioners;
using GUIProject.Models;
using GUIProject.Other;
using System;
using System.Collections.Generic;
using System.Text;

namespace GUIProject.ViewModels
{
    public class XyzAxisMultiplierViewModel
    {
        
        public GuiProperty<double> X { get => _x; set => _x = value; }
        public GuiProperty<double> Y { get => _y; set => _y = value; }
        public GuiProperty<double> Z { get => _z; set => _z = value; }
        GuiProperty<double> _x = new GuiProperty<double>();
        GuiProperty<double> _y = new GuiProperty<double>();
        GuiProperty<double> _z = new GuiProperty<double>();
        double modifier = 0.1;
        XyzConstrains _model = new XyzConstrains();
        public Action Close { set; private get; }
        public XyzAxisMultiplierViewModel()
        {
            Refresh();
        }

        private void Refresh()
        {
            X.Value = _model.X.Value;
            Y.Value = _model.Y.Value;
            Z.Value = _model.Z.Value;
        }

        public void SetValues()
        {
            _model.X= _x;
            _model.Y = _y;
            _model.Z = _z;
        }
        
        public void IncreaseX()
        {
            _model.IncreaseX(modifier);
            Refresh();
        }
        public void IncreaseY()
        {
            _model.IncreaseY(modifier);
            Refresh();
        }
        public void IncreaseZ()
        {
            _model.IncreaseZ(modifier);
            Refresh();
        }
        public void DecreaseX()
        {
            _model.IncreaseX(-modifier);
            Refresh();
        }
        public void DecreaseY()
        {
            _model.IncreaseY(-modifier);
            Refresh();
        }
        public void DecreaseZ()
        {
            _model.IncreaseZ(-modifier);
            Refresh();
        }
        public void Apply()
        {
            SetValues();
        }
        public void CloseWindow()
        {
            Close();
        }
    }
}

using Avalonia.Controls;
using Avalonia.Media;
using GUIProject.Other;
using ReactiveUI;
using System;
using System.Reactive;

namespace GUIProject.ViewModels
{
    public class ColorDialogViewModel:ViewModelBase
    {
        private GuiProperty<byte> _r = new GuiProperty<byte>();
        private GuiProperty<byte> _g = new GuiProperty<byte>();
        private GuiProperty<byte> _b = new GuiProperty<byte>();
        public GuiProperty<byte> R { get => _r; set { SetColor(); _r = value; } }
        public GuiProperty<byte> G { get=>_g; set { SetColor(); _g = value; } }
        public GuiProperty<byte> B { get => _b; set { SetColor(); _b = value; } }
        public GuiProperty<SolidColorBrush> Brush { get; set; }
        public GuiProperty<Color> Color { get; set; }

        public static ReactiveCommand<Unit, Unit> Command { get; set; }
        public Action<object> Close;

        public ColorDialogViewModel()
        {
            Color = new GuiProperty<Color>() { Value = new Color(255, R.Value, G.Value, B.Value) };
            Command = ReactiveCommand.Create(SetColor);
            Brush = new GuiProperty<SolidColorBrush>() { Value = new SolidColorBrush(Color.Value) };
            _r.PropertyChanged += RefreshColor;
            _g.PropertyChanged += RefreshColor;
            _b.PropertyChanged += RefreshColor;
        }

        private void RefreshColor(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SetColor();
        }


        public void SetColor()
        {
            Color.Value = new Color(255, R.Value, G.Value, B.Value);
            Brush.Value = new SolidColorBrush(Color.Value);

        }
        public void CloseDialog()
        {
            Close(Color.Value);
        }
        public void CancelDialog()
        {
            Close(null);
        }
    }
}

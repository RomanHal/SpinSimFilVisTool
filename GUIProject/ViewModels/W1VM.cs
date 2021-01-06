using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using GUIProject.Other;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Timers;

namespace GUIProject.ViewModels
{
    public class W1VM:ViewModelBase
    {
        private Window _window;
        public GuiProperty<byte> R { get; set; }
        public GuiProperty<byte> G { get; set; }
        public GuiProperty<byte> B { get; set; }
        public GuiProperty<SolidColorBrush> Brush { get; set; }
        Timer timer;

        string asdf = "asdfw";
        public string Aaa => "asdfff";
        
        public GuiProperty<Color> Color { get; set; }
        //Color Color = new Color();
        string test = "test text";
        public string Test { get => test; set => test = value; }

         public static ReactiveCommand<Unit, Unit> Command { get; set; }
        public W1VM(Window window)
        {
            _window = window;
            R = new GuiProperty<byte>() {Value=255 };
            G = new GuiProperty<byte>();
            B = new GuiProperty<byte>();
            Color = new GuiProperty<Color>() { Value = new Color(255,R.Value, G.Value, B.Value) };
            Command = ReactiveCommand.Create(SetText);
            Brush = new GuiProperty<SolidColorBrush>() { Value = new SolidColorBrush(Color.Value) };
            Brush.Value.Color = Color.Value;
            timer = new Timer(500)
            {
                AutoReset = true
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        public W1VM():this(null)
        {
            Window window=null;
            IControl control = (Control)window;
            Window w2 = (Window)control;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.UIThread.InvokeAsync(() => SetText());

        }

        public void SetText()
        {
            Color.Value = new Color(255, R.Value, G.Value, B.Value);
            Brush.Value =new SolidColorBrush( Color.Value);
            
        }
        public void CloseDialog()
        {
            _window.Close(Color);
        }
    }
}

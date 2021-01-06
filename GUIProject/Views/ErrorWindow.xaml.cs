using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GUIProject.Other;
using System;

namespace GUIProject.Views
{
    public class ErrorWindow : Window
    {
        private readonly Exception _exception;

        public GuiProperty<string> ErrorType { get=>_errorType; set=>_errorType=value; }
        GuiProperty<string> _errorType = new GuiProperty<string>();
        public GuiProperty<string> ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
        GuiProperty<string> _errorMessage = new GuiProperty<string>();
        public ErrorWindow(Exception e)
        {
            _exception = e;

            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = this;
            ErrorMessage.Value = _exception.Message ;
            ErrorType.Value="Exception " + _exception.GetType() 
                + " in " + _exception.Source ;
        }
        public void CloseWindow()
        {
            this.Close();
        }
    }
}

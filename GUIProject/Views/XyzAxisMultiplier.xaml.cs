﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GUIProject.ViewModels;

namespace GUIProject.Views
{
    public class XyzAxisMultiplier : Window
    {
        public XyzAxisMultiplier()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = new XyzAxisMultiplierViewModel() { Close=Close };
        }
    }
}

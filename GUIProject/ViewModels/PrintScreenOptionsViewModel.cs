using Avalonia.Controls;
using GUIProject.Other;
using System;
using System.Collections.Generic;
using System.Text;

namespace GUIProject.ViewModels
{
    public class PrintScreenOptionsViewModel:ViewModelBase
    {
        public Action<string> Close { get; set; }
        public Action Cancel { get; set; }
        public GuiProperty<bool> IsSelectedCustom { get => _isSelectedCustom; set => _isSelectedCustom = value; }
        public GuiProperty<string> CustomPath { get => _customPath; set => _customPath = value; }
        public GuiProperty<string> DefaultPath { get => _defaultPath; set => _defaultPath = value; }


        GuiProperty<bool> _isSelectedCustom = new GuiProperty<bool>();
        GuiProperty<string> _customPath = new GuiProperty<string>();
        GuiProperty<string> _defaultPath = new GuiProperty<string>(@"PrintScreens/");

        public PrintScreenOptionsViewModel(string patch)
        {
            if (patch == _defaultPath.Value) _isSelectedCustom.Value = false;
            else
            {
                _isSelectedCustom.Value = true;
                _customPath.Value = patch;
            }
        }
        public PrintScreenOptionsViewModel()
        {

        }
        public async void SetDirectory()
        {
            var pathSelector = new OpenFolderDialog
            {
                InitialDirectory = "",
            };
            var path = await pathSelector.ShowAsync();
            if (path == null)
            {
                IsSelectedCustom.Value = false;
            }
            else CustomPath.Value = path;
        }

        public void Ok()
        {
            if (IsSelectedCustom.Value)
            {
                Close(CustomPath.Value);
            }
            else Close(DefaultPath.Value);
        }
        public void CancelSelect()
        {
            Cancel();
        }
    }
}

using Avalonia.Controls;
using CoreLibrary.FileServices;
using GUIProject.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUIProject.ViewModels
{
    public class TextWindowViewModel:ViewModelBase
    {
        public Action Close { get; set; }
        public GuiProperty<string> Text { get => _text; set => _text = value; }
        GuiProperty<string> _text = new GuiProperty<string>();

        public TextWindowViewModel()
        {

        }
        public TextWindowViewModel(params string[] text):this()
        {
            if (text != null)
            {
                _text.Value = string.Join(Environment.NewLine, text);
            }
        }

        public void CloseWindow()
        {
            Close();
        }
        private List<FileDialogFilter> GetFileDialogFiltersList()
        {
            var mif = new FileDialogFilter
            {
                Name = "mif",
                Extensions = new List<string>() { "mif" }
            };
            var all = new FileDialogFilter
            {
                Name = "all",
                Extensions = new List<string>() { "*" }
            };
            var text = new FileDialogFilter
            {
                Name = "text",
                Extensions = new List<string>() { "txt" }
            };
            return new List<FileDialogFilter>() { mif, text, all };
        }

        public async void Save()
        {
            var saveDialog = new SaveFileDialog
            {
                Filters = GetFileDialogFiltersList(),
                DefaultExtension = "mif"
            };

            var patch = await saveDialog.ShowAsync(null);
            if (patch != null || patch != "")
            {
                var text = Text.Value.Split(Environment.NewLine);
                new FileSaver().Save(text, patch);
            }
        }
    }
}

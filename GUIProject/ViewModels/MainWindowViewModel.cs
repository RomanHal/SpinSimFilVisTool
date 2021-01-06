using Avalonia.Controls;
using Avalonia.Threading;
using CoreLibrary.FileServices;
using CoreLibrary.GraphicsServices;
using CoreLibrary.GraphicsServices.Interfaces;
using CoreLibrary.Interfaces;
using GUIProject.Models;
using GUIProject.Other;
using GUIProject.Views;
using System;
using System.Collections.Generic;

namespace GUIProject.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public GuiProperty<bool> IsGraphicWindowOpen { get=>_isGraphicWindowOpen; set => _isGraphicWindowOpen = value; }
        public GuiProperty<string> Path { get => _path; set => _path = value; }

        public Action Close { get; set; }
        GuiProperty<bool> _isGraphicWindowOpen = new GuiProperty<bool>();
        IPrintScreenMaker _printScreener = new PrintScreener(); 
        GuiProperty<string> _path = new GuiProperty<string>(@"PrintScreens/");

        private ClickHandlerHelper _clickHandler= new ClickHandlerHelper();
        private IViewCreator _creator = new ViewLocator();

        public MainWindowViewModel()
        {
            DataStorage.ObjectContainer.OnClickSelectMethod = _clickHandler.HandleClick;
        }

        public void OpenAxisMultiplier()
        {
            var dialog = (Window)_creator.Build(new XyzAxisMultiplierViewModel());
            dialog.Show();
        }

        public void CreateNew()
        {
            DataStorage.ObjectContainer.Clear();
        }

        public void OpenEnergyWindow()
        {
            var window = new Energy();
            window.Show();
        }
        public async void SetPrintScreenPatch()
        {
            var dialog = (Window)_creator.Build(new PrintScreenOptionsViewModel(), Path.Value);
            var path = await dialog.ShowDialog<string>();
            if (path != null) Path.Value = path;
        }
        public void PrintScreen()
        {
            _printScreener.PrintScreen(Path.Value);
        }
        public void OpenVisualizationWindow()
        {
            var window = new GraphicWindow();
            window.OpenWindow(800, 600);
        }
        public void OpenAtlasWindow()
        {
            Window window =(Window) _creator.Build(new AtlasViewModel());
            window.Show();
        }
        public void OpenScriptWindow()
        {
            var window = new NewScript();
            window.Show();
        }
        public async void OpenFile()
        {
            var fileDialog = new OpenFileDialog
            {
                Filters = GetFileDialogFiltersList(),
                AllowMultiple = false
            };
            var paths = await fileDialog.ShowAsync();
            if (paths != null)
            {
                //try
                {
                    var fileLoader = new FileLoader();
                    DataStorage.ObjectContainer = fileLoader.LoadFile(paths[0]);
                }
                //catch(Exception exception)
                {
                    // new ErrorWindow(exception).Show();
                }
                DataStorage.ObjectContainer.OnClickSelectMethod = _clickHandler.HandleClick;

            }
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
        public async void SaveFileAs()
        {
            var saveDialog = new SaveFileDialog
            {
                Filters=GetFileDialogFiltersList(),
                DefaultExtension = "mif"
            };
            
            var patch=await saveDialog.ShowAsync(null);
            if(patch!=null||patch!="")
            new FileSaver().Save(DataStorage.ObjectContainer, patch);
        }
        public void OpenPreViewWindow()
        {
            var text = new FileSaver().Preview(DataStorage.ObjectContainer);
            var window = (Window)_creator.Build(new TextWindowViewModel(),(string[])text);
            window.Show();
        }
        public void OpenUninterpretedWindow()
        {
            var window = (Window)_creator.Build(new UnInterpretedTextViewModel());
            window.Show();
        }
        public void Exit()
        {
            Close();
        }
    }
}

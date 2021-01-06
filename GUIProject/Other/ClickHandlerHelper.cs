using Avalonia.Controls;
using Avalonia.Threading;
using CoreLibrary.Interfaces;
using GUIProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GUIProject.Other
{
    class ClickHandlerHelper
    {
        private IViewCreator _creator = new ViewLocator();

        public void HandleClick(object @object, Type type)
        {
            if (type == typeof(IObject)) OpenAtlas(@object, type);
            if (type == typeof(IEnergy)) OpenEnergy(@object, type);
        }
        void OpenAtlas(object @object, Type type)
        {
                var atlas = @object as IObject;
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Window window = (Window)_creator.Build(new AtlasViewModel(), atlas);
                    window.Show();
                    window.Focus();
                });
        }
        void OpenEnergy(object @object, Type type)
        {
                var energy = @object as IEnergy;
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Window window = null;
                    switch (energy.Type)
                    {
                        case CoreLibrary.Enums.EnergyType.UZeeman:
                            window = (Window)_creator.Build(new ZeemanViewModel(), energy);
                            break;
                        case CoreLibrary.Enums.EnergyType.UniaxialAnisotropy:
                            window = (Window)_creator.Build(new AnisotropyViewModel(), energy);
                            break;
                        case CoreLibrary.Enums.EnergyType.TwoSurfaceExchange:
                            window = (Window)_creator.Build(new ExchangeViewModel(), energy);
                            break;
                    }
                    window?.Show();
                    window.Focus();
                });
        }
    }
}

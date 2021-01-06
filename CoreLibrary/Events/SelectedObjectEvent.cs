using CoreLibrary.Interfaces;
using GraphicLibrary.Interfaces;
using GraphicLibrary.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreLibrary.Events
{
    public delegate void OnClickSelect(object @object, Type type);
    public class SelectedObjectEvent
    {
        private readonly List<IEnergy> _energies;
        private readonly List<IObject> _objects;
        
        public OnClickSelect OnClickSelectMethod;
        public SelectedObjectEvent(List<IEnergy> energies,List<IObject> objects)
        {
            this._energies = energies;
            this._objects = objects;
            SelectedObjectManagement.OnClickSelectMethod = OnSelectionEvent;
        }
        public void OnSelectionEvent(IGraphicsObject graphicsObject)
        {
            var atlas= _objects.Where(o => o is SpintronicsObject)
                .Where(o => (o as SpintronicsObject).GraphicsObject == graphicsObject).FirstOrDefault();
            var energy = _energies.Where(e=>e is SpintronicsEnergy)
                .Where(o => (o as SpintronicsEnergy).GraphicsObjects.Any(g => g == graphicsObject)).FirstOrDefault();
            if (atlas != null)
                OnClickSelectMethod(atlas, typeof(IObject));
            else OnClickSelectMethod(energy, typeof(IEnergy));
        }
    }
}

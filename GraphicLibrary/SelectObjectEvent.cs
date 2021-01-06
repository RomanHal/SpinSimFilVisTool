using GraphicLibrary.Configuration;
using GraphicLibrary.Interfaces;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphicLibrary
{
    class SelectObjectEvent
    {
        public IGraphicsObject SelectedObject { get { return _selectedObject; } }
        IGraphicsObject _selectedObject;
        MouseDevice _mouse;
        List<IGraphicsObject> _renderedObjects;
        Camera _camera;
        public event EventHandler<SelectObjectEventArgs> OnSelectionChange;
        public SelectObjectEvent(List<IGraphicsObject> renderedObjects, MouseDevice mouse,Camera camera)
        {
            _renderedObjects = renderedObjects;
            _mouse = mouse;
            _camera = camera;
            mouse.ButtonUp += OnClick;
        }

        public void OnClick(object sender,MouseButtonEventArgs e)
        {
            if(e.Button==MouseButton.Left)
            {
                _selectedObject = GetNearestObject(GetOnRaycast(GetVisibleObjects()));
                if(_selectedObject!=null)
                OnSelectionChange?.Invoke(this,new SelectObjectEventArgs() { SelectedObject = _selectedObject });
            }
        }

        Vector3 GetRaycastDirection()
        {
            return _camera.RaycastDirection();
        }

        Vector3 GetCoursorPosition()
        {
            return _camera.MouseToWorldCoords();
        }

        List<ClickableObject> GetVisibleObjects()
        {
            return _renderedObjects.Where(n => n.Visible == true).Where(n => n is ClickableObject)
                .Select(e => (ClickableObject)e).ToList();

        }

        List<ClickableObject> GetOnRaycast(List<ClickableObject> objects)
        {

            List<ClickableObject> onRaycast = new List<ClickableObject>();
            foreach (ClickableObject shape in objects)
            {
                float distance =(float) shape.GetDistanceFrom(GetCoursorPosition());
                Vector3 point = GetCoursorPosition() + distance * GetRaycastDirection();
                if (shape.PointBelongsTo(point)) onRaycast.Add(shape);
            }
            return onRaycast;
        }

        IGraphicsObject GetNearestObject(List<ClickableObject> objectsOnRaycast)
        {
            return objectsOnRaycast.Select(s => s).OrderBy(e => e.GetDistanceFrom(GetCoursorPosition())).FirstOrDefault();
        }
        
    }

    public class SelectObjectEventArgs : EventArgs
    {
        public IGraphicsObject SelectedObject { get; set; }
    }
}

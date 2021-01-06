using GraphicLibrary.Interfaces;
using GraphicLibrary.Management;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GraphicLibrary.Management
{
    public static class FigureContainer
    {
        public static double height;
        public static double radious;
        volatile static List<IGraphicsObject> _elements= new List<IGraphicsObject>();

        public static ColorDictionary colorDictionary=new ColorDictionary();
        public static void ClearContainer()
        {
            _elements.Clear();
        }
        public static int GetAmountOfElements()
        {
            return _elements.Count;
        }
        public  static List<IGraphicsObject> GetElements()
        {
            return  _elements;
        }
        public static void Add(IGraphicsObject @object)
        {
            _elements.Add(@object);
        }
        public static void Remove(IGraphicsObject @object)
        {
            _elements.Remove(@object);
        }
        public static void AddElement(string name,FigureColor color, FigureType type, Point FirstPoint, Point LastPoint, object data,int[] RGBcolor)
        {
            //TODO

            if(type==FigureType.Cylinder)
            {
                _elements.Add(new Cylinder(FirstPoint, LastPoint, (double)data, colorDictionary.GetColor(color, RGBcolor)) {Name=name });
                _elements[_elements.Count - 1].Id = name;

            }
            if(type==FigureType.Cuboid)
            {
                double[] dataArray =(double[]) data;
                _elements.Add(new Cube(FirstPoint, LastPoint, dataArray[0], dataArray[1], colorDictionary.GetColor(color, RGBcolor)) {Name=name });
                
            }
         //   throw new NotImplementedException();
        }
        public static void DeleteElement(int index)
        {
            _elements.RemoveAt(index);
        }
        
        public static IGraphicsObject GetElement(int index)
        {
            return _elements[index];
        }
        //TODO: Delete \/
        public static CloseWindowEvent closeWindow = new CloseWindowEvent();
        public static MoveWindowEvent moveWindow = new MoveWindowEvent();
        public static ReturnFocusEvent returnFocus = new ReturnFocusEvent();//not used
    }
}

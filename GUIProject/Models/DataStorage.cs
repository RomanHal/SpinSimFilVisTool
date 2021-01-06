using CoreLibrary;
using CoreLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GUIProject.Models
{
    public static class DataStorage
    {
        public static IObjectContainer ObjectContainer { get => _container; set => _container = value; }
        static IObjectContainer _container = new ObjectsContainer();
        static DataStorage()
        {
        }

    }
}

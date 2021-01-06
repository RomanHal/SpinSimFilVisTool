using Avalonia.Media;
using CoreLibrary.Enums;
using CoreLibrary.Factories;
using CoreLibrary.Interfaces;
using GUIProject.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GUIProject.Models
{
    public class AtlasModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> Atlases { get => DataStorage.ObjectContainer.ObjectNames; }
        public List<string> MultiAtlases { get => DataStorage.ObjectContainer.MultiAtlases; }
        public IObject GetAtlas(string name)
        {
            return DataStorage.ObjectContainer.GetObject(name);
        }

        public void Add(string name,AtlasType type,double xMin,double xMax, double yMin, 
            double yMax,  double zMin, double zMax,Color color,string regions,
            string scriptName,string scriptArgs,string multiAtlas)
        {
            var regionList =(regions!="")? new List<string>(regions.Split(' ',
                StringSplitOptions.RemoveEmptyEntries)):null;
            IScript script =scriptName!=null? DataStorage.ObjectContainer
                .GetScript(scriptName):null;
            var factory = new ObjectFactory();
            IObject atlas= factory.Create(name,type,xMin,xMax,yMin,yMax,zMin,zMax,
                GetIColor(color),regionList, script,scriptArgs);
            atlas.Multiatlas=multiAtlas;
            Add(atlas);
        }

        public void Add(IObject atlas)
        {
            DataStorage.ObjectContainer.Add(atlas);
            OnPropertyChanged(nameof(Atlases));
        }
        public void Delete(string atlasName)
        {
            Delete(GetAtlas(atlasName));
        }
        public void Delete(IObject atlas)
        {
            DataStorage.ObjectContainer.Delete(atlas);
            OnPropertyChanged(nameof(Atlases));
        }
        public void Update(string name,string newName,double xMin,double xMax, double yMin, 
            double yMax, double zMin, double zMax,Color color,bool visible,string multiAtlas)
        {
            var atlas = GetAtlas(name);
            atlas.Name = newName;
            atlas.Xmin=xMin;
            atlas.Xmax = xMax;
            atlas.Ymin = yMin;
            atlas.Ymax = yMax;
            atlas.Zmin = zMin;
            atlas.Zmax = zMax;
            atlas.Color = GetIColor(color);
            atlas.Visible = visible;
            atlas.Multiatlas = multiAtlas;
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private IColor GetIColor(Color color)
        {
            return new GuiColor(color);
        }

        


    }
}

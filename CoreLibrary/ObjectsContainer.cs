using CoreLibrary.Events;
using CoreLibrary.Interfaces;
using CoreLibrary.Positioners;
using GraphicLibrary.Management;
using MifParser;
using MifParser.Atlases;
using MifParser.Factories;
using MifParser.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace CoreLibrary
{
    public class ObjectsContainer : IObjectContainer
    {
        List<IObject> _objects = new List<IObject>();
        List<IEnergy> _energies = new List<IEnergy>();
        List<IScript> _scripts = new List<IScript>();

        MifContainerGetter containerGetter;
        CorePositioner _positioner;

        Timer _timer = new Timer(1000);

        public List<string> ObjectNames => _objects.Select(o=>o.Name).ToList();
        public List<string> EnergieNames => _energies.Select(e=>e.Name).ToList();
        public List<string> ScriptsNames => _scripts.Select(s=>s.Name).ToList();
        public List<string> MultiAtlases { get=>_objects.Where(o=>o.Multiatlas!=null).
                Where(o=>o.Multiatlas!="").Select(o=>o.Multiatlas).Distinct().ToList(); }
        public List<string> Regions => _objects.SelectMany(o => o.Regions).Distinct().ToList();
        public List<List<string>> UnprocessedText { get; set ; }
        public OnClickSelect OnClickSelectMethod { set =>_selectedObjectEvent.OnClickSelectMethod=value ; }
        SelectedObjectEvent _selectedObjectEvent; 
        public ObjectsContainer()
        {
            _selectedObjectEvent =new SelectedObjectEvent(_energies, _objects);
            UnprocessedText = new List<List<string>>() {new List<string>() {""}, new List<string>() { "" },
            new List<string>() {""},new List<string>() {""}};
            containerGetter = new MifContainerGetter(_objects, _energies, _scripts);
            _positioner = new CorePositioner(_objects, _energies, new List<IEnergyPositioner>
            { new AnisotropyPositioner(_objects, _energies) });
            _timer.AutoReset = true;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
           if(_objects.Count>0) _positioner.Refresh();
        }

        public void Clear()
        {
            _energies.Clear();
            _objects.Clear();
            _scripts.Clear();
            UnprocessedText = new List<List<string>>() {new List<string>() {""}, new List<string>() { "" },
            new List<string>() {""},new List<string>() {""}};
            FigureContainer.ClearContainer();
        }
        public void Add(IObject @object)
        {
            FigureContainer.Add(((SpintronicsObject)@object).GraphicsObject);
            _objects.Add(@object);
            _positioner.Refresh();
        }

        public void Add(IEnergy energy)
        {
            foreach(var graphic in (energy as SpintronicsEnergy).GraphicsObjects)
            {
                FigureContainer.Add(graphic);
            }
            _energies.Add(energy);
            _positioner.Refresh();
        }

        public void Add(IScript script)
        {
            _scripts.Add(script);
        }

        public void Delete(IObject @object)
        {
            FigureContainer.Remove(((SpintronicsObject)@object).GraphicsObject);
            _objects.Remove(@object);
            _objects.ForEach(o => o.Refresh());
            _positioner.Refresh();

        }

        public void Delete(IEnergy energy)
        {
            foreach (var graphic in (energy as SpintronicsEnergy).GraphicsObjects)
            {
                FigureContainer.Remove(graphic);
            }
            _energies.Remove(energy);
            _positioner.Refresh();
        }

        public void Delete(IScript script)
        {
            _scripts.Remove(script);
        }

        public IEnergy GetEnergy(string name)
        {
            return _energies.Where(e => e.Name == name).FirstOrDefault();
        }

        public IObject GetObject(string name)
        {
            return _objects.Where(o => o.Name == name).FirstOrDefault();
        }

        public IScript GetScript(string name)
        {
            return _scripts.Where(s => s.Name == name).FirstOrDefault();
        }

        internal MifObjectsContainer GetContainer()
        {
            return containerGetter.GetContainer(MultiAtlases, UnprocessedText);
        }
    }
}

using CoreLibrary.FileServices.Interfaces;
using CoreLibrary.GraphicsServices;
using CoreLibrary.GraphicsServices.Interfaces;
using CoreLibrary.Interfaces;
using GraphicLibrary.Interfaces;
using GraphicLibrary.Management;
using MifParser.Atlases;
using MifParser.Interfaces;
using System;
using System.Linq;

namespace CoreLibrary
{
    class Converter : IConverter
    {
        MifParser.MifObjectsContainer _container;
        IGraphicCreator _graphicCreator = new GraphicCreator();
        EnergyCreator _energyCreator = new EnergyCreator();

        public IObjectContainer Container { get; private set; }
        public Converter(MifParser.MifObjectsContainer objectsContainer)
        {
            _container = objectsContainer;
        }

        public void Convert()
        {
            FigureContainer.ClearContainer();
            IColor color=null;
            Container = new ObjectsContainer();
            _container.MifAtlases.Where(a=>!(a is MultiAtlas)).ToList().ForEach(atlas => 
            {
                var graphic = AddGraphic(atlas, color);
                Container.Add(new SpintronicsObject(atlas, graphic));
            });
            _container.MifAtlases.Where(a => a is MultiAtlas).ToList().ForEach(m =>
              ((MultiAtlas)m).MifAtlas.ForEach(atlas =>
              {
                  var graphic = AddGraphic(atlas,color);
                  Container.Add(new SpintronicsObject(atlas, graphic,m.Name));
              }));
            _container.MifEnergies.ForEach(energy =>
            {
                Container.Add(CreateEnergy(energy, color));
            });
            _container.MifScripts.ForEach(script =>
            {
                Container.Add(new MifScript(script)); 
            });
            Container.UnprocessedText = _container.UnprocessedText.Text;//TODO:rework

        }

        public IGraphicsObject AddGraphic(IMifAtlas atlas,IColor color)
        {
            if(atlas!=null)
            return _graphicCreator.CreateGraphic(atlas,color);
            throw new NullReferenceException();
        }
        public IEnergy CreateEnergy(IMifEnergy energy, IColor color)
        {
            if (energy != null)
            {
                return _energyCreator.CreateEnergy(energy, color);
            }
            throw new NullReferenceException();
        }

    }
}

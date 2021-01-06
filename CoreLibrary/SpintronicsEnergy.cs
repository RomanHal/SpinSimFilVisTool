using CoreLibrary.Enums;
using CoreLibrary.GraphicsServices;
using CoreLibrary.Interfaces;
using GraphicLibrary.Interfaces;
using MifParser.Interfaces;
using System.Collections.Generic;

namespace CoreLibrary
{
    public abstract class SpintronicsEnergy : IEnergy
    {
        protected IMifEnergy _energy;
        internal IMifEnergy Energy => _energy;
        public string Name { get => _energy.Name; set => _energy.Name = value; }
        public abstract EnergyType Type { get; }
        public IColor Color { get => new CoreColor(GraphicsObjects[0].Color);
            set => SetColors(value); }



        internal abstract List<IGraphicsObject> GraphicsObjects { get; }
        public SpintronicsEnergy()
        {
        }


        public SpintronicsEnergy(IMifEnergy energy):this()
        {
            _energy = energy;
        }
        public abstract void AddEnergyPart(IEnergyPart part);
        public abstract void RemoveEnergyPart(IEnergyPart part);
        OpenTK.Color GetColor(IColor color)
        {
            return new OpenTK.Color(color.R, color.G, color.B, 255);
        }
        private void SetColors(IColor value)
        {
            GraphicsObjects.ForEach(g => g.Color = GetColor(value));
        }
    }
}

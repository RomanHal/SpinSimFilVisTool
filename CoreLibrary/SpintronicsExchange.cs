using System;
using System.Collections.Generic;
using System.Text;
using CoreLibrary.Enums;
using CoreLibrary.Interfaces;
using GraphicLibrary.Interfaces;
using MifParser.Energies;
using MifParser.Interfaces;

namespace CoreLibrary
{
    class SpintronicsExchange : SpintronicsEnergy
    {
        
        public override EnergyType Type => EnergyType.TwoSurfaceExchange;

        public double Sigma1 { get; set; }
        public double Sigma2 { get; set; }
        public string Atlas { get; set; }
        public string Region { get; set; }
        public double Norm { get; set; }
        public double Fieldvalue { get; set; }
        public double ScalarVectorX { get; set; }
        public double ScalarVectorY { get; set; }
        public double ScalarVectorZ { get; set; }
        public double ScalarValue { get; set; }
        public bool Sign { get; set; }
        public IColor Color { get; set; }
        internal override List<IGraphicsObject> GraphicsObjects => new List<IGraphicsObject>() { _graphic };
        private IGraphicsObject _graphic;
        public SpintronicsExchange()
        { }
        public SpintronicsExchange(IMifEnergy energy,IGraphicsObject graphic)
        {
            _energy = energy;
            _graphic = graphic;

        }


        public override void AddEnergyPart(IEnergyPart part)
        {
            throw new NotImplementedException();
        }

        public override void RemoveEnergyPart(IEnergyPart part)
        {
            throw new NotImplementedException();
        }
    }
}

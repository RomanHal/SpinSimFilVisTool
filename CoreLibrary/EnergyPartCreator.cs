using CoreLibrary.Enums;
using CoreLibrary.GraphicsServices;
using CoreLibrary.Interfaces;
using GraphicLibrary.Interfaces;
using GraphicLibrary.Management;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary
{
    public class EnergyPartCreator
    {
        GraphicCreator _creator = new GraphicCreator();
        public IEnergyPart Create(EnergyType type,double directionX, double directionY, double directionZ,
            IColor color,double value, string region)
        {
            IGraphicsObject graphic = _creator.CreateGraphic(type, value, directionX, directionY, directionZ, color);
            return new EnergyPart(graphic, region, value);
        }
    }
}

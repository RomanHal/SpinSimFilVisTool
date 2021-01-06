using System;
using System.Collections.Generic;
using System.Linq;
using CoreLibrary.Enums;
using CoreLibrary.GraphicsServices.Interfaces;
using CoreLibrary.Interfaces;
using CoreLibrary.Positioners;
using GraphicLibrary;
using GraphicLibrary.Interfaces;
using MifParser.Atlases;
using MifParser.Energies;
using MifParser.Interfaces;

namespace CoreLibrary.GraphicsServices
{
    class GraphicCreator : IGraphicCreator
    {
        FigureFactory _factory = new FigureFactory();
        GraphicDataCalculler _calculler = new GraphicDataCalculler();
        public IGraphicsObject CreateGraphic(IMifAtlas atlas,IColor color)
        {
            OpenTK.Color colorOpenTk = GetColor(color);
            switch (atlas.GetType())
            {
                case Type T when T == typeof(BoxAtlas):
                    return CreateCuboid(atlas,colorOpenTk);
                case Type T when T == typeof(ScriptAtlas):
                    return CreateCylinder(atlas,colorOpenTk);
                default: throw new NotSupportedException("Atlas Type: " + atlas.GetType() + " is not supported");
            }
        }

        private OpenTK.Color GetColor(IColor color)
        {
            return color==null?OpenTK.Color.Blue:new OpenTK.Color(color.R, color.G, color.B, 255);
        }

        private IGraphicsObject CreateCuboid(IMifAtlas atlas, OpenTK.Color color)
        {
            var figureType = FigureType.Cuboid;
            var shapeData= _calculler.Calculate(atlas.X, atlas.Y, atlas.Z,false,1,1,1,XyzConstrainsModifier.Norm);
            return _factory.Create(figureType, shapeData.Item1, shapeData.Item2, shapeData.Item3, shapeData.Item4, color);
        }
        private IGraphicsObject CreateCylinder(IMifAtlas atlas, OpenTK.Color color)
        {
            var scriptAtlas = (ScriptAtlas)atlas;
            if (scriptAtlas.Script == null) throw new FormatException("ScriptAtlas " + atlas.Name
                + " doesn't have script.");
            var tuple= _calculler.Calculate(atlas.X, atlas.Y, atlas.Z,true);
            if(!(scriptAtlas.Script.Name.Contains("Ellipse")))
            {
                throw new FormatException("Unknown script " + scriptAtlas.Script.Name +" is in Oxs_ScriptAtlas "
                    + scriptAtlas.Name + ". Only EllipseX, EllipseY, EllipseZ are supported");
            }
            return _factory.Create(FigureType.Cylinder, tuple.Item1, tuple.Item2,tuple.Item3, tuple.Item4, color);
        }

        public IGraphicsObject CreateGraphic(EnergyType type, double value,double versorX,
            double versorY,double versorZ,IColor color)
        {
            switch(type)
            {
                case EnergyType.UniaxialAnisotropy:
                    return CreateDoubleArrow(value, versorX, versorY, versorZ, color);
                case EnergyType.UZeeman:
                    return CreateArrow(value, versorX, versorY, versorZ,color);
                default: throw new NotImplementedException();
            }
        }
        private IGraphicsObject CreateDoubleArrow(double value,double directionX,double directionY, 
            double directionZ, IColor color)
        {
            return _factory.Create(FigureType.DoubleArrow, new Point(0, 0, 0), 
                new Point(directionX, directionY, directionZ), 0, 0, GetColor(color));
        }

        private IGraphicsObject CreateArrow(double value, double versorX, double versorY, double versorZ, IColor color)
        {
            Point point = new Point(versorX, versorY, versorZ);
            return _factory.Create(FigureType.Arrow, point, new Point(0, 0, 0), 
                point.Length*0.1 , 0, GetColor(color));
        }
    }
}

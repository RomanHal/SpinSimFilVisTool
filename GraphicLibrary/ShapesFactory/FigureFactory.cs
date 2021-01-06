using GraphicLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicLibrary
{
    public class FigureFactory
    {
        ColorDictionary _colorDictionary = new ColorDictionary();
        public IGraphicsObject Create(FigureType type, Point firstPoint,Point lastPoint,double d1,double d2,FigureColor color)
        {
            switch(type)
            {
                case FigureType.Cuboid:
                    return CreateCuboid(firstPoint, lastPoint, d1, d2,_colorDictionary.GetColor(color));
                case FigureType.Cylinder:
                    return CreateCylinder(firstPoint,lastPoint,d1,d2, _colorDictionary.GetColor(color));
                case FigureType.Arrow:
                    return CreateArrow(firstPoint, lastPoint,d1, _colorDictionary.GetColor(color));
                case FigureType.DoubleArrow:
                    return CreateDoubleArrow(firstPoint, lastPoint,d1, _colorDictionary.GetColor(color));
                case FigureType.Tetrahedron:
                    return new Tetrahedron(firstPoint, lastPoint, _colorDictionary.GetColor(color), d1);
                default:
                    throw new Exception("Chosen ShapeType do not exist");
            }

        }
        public IGraphicsObject Create(FigureType type, Point firstPoint, Point lastPoint, double d1, double d2, OpenTK.Color color)
        {
            switch (type)
            {
                case FigureType.Cuboid:
                    return CreateCuboid(firstPoint, lastPoint, d1, d2, color);
                case FigureType.Cylinder:
                    return CreateCylinder(firstPoint, lastPoint, d1, d2, color);
                case FigureType.Arrow:
                    return CreateArrow(firstPoint, lastPoint, d1, color);
                case FigureType.DoubleArrow:
                    return CreateDoubleArrow(firstPoint, lastPoint, d1, color);
                case FigureType.Tetrahedron:
                    return new Tetrahedron(firstPoint, lastPoint, color, d1);
                default:
                    throw new Exception("Chosen ShapeType do not exist");
            }

        }
        Cube CreateCuboid(Point firstPoint, Point lastPoint, double d1, double d2,OpenTK.Color color) 
            => new Cube(firstPoint, lastPoint, d1, d2, color);
        Cylinder CreateCylinder(Point firstPoint, Point lastPoint, double d1, double d2, OpenTK.Color color)
            => new Cylinder(firstPoint, lastPoint, d1, color);
        Arrow CreateArrow(Point firstPoint, Point lastPoint, double d1, OpenTK.Color color)
            => new Arrow(firstPoint, lastPoint,d1, color);
        DoubleArrow CreateDoubleArrow(Point firstPoint, Point lastPoint, double d1, OpenTK.Color color)
            => new DoubleArrow(firstPoint, lastPoint, color);
        Tetrahedron CreateTetrahedron(Point firstPoint, Point lastPoint, double d1, OpenTK.Color color)
            => new Tetrahedron(firstPoint, lastPoint, color, d1);

    }
}

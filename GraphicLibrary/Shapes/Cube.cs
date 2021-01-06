using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GraphicLibrary
{
    class Cube:ClickableObject
    {
       
        private double _height;


        public Cube(Point firstPoint, Point lastPoint, double widthD1, double widthD2, Color color)
            :base(firstPoint,lastPoint
                 , widthD1, widthD2,  color ,FigureType.Cuboid)
        {
        }

        protected override void DrawSchema()
        { 
            var vector = _lastPoint- _firstPoint ;
            _height = vector.Length;

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color);
            GL.Normal3(1.0f, 0.0, 0.0);
            GL.Vertex3(D1 / 2, -D2 / 2, _height);
            GL.Vertex3(D1 / 2, -D2 / 2, 0);
            GL.Vertex3(D1 / 2, D2 / 2, 0);
            GL.Vertex3(D1 / 2, D2 / 2, _height);

            GL.Normal3(-1.0f, 0.0f, 0.0f);
            GL.Vertex3(-D1 / 2, -D2 / 2, _height);
            GL.Vertex3(-D1 / 2, -D2 / 2, 0);
            GL.Vertex3(-D1 / 2, D2 / 2, 0);
            GL.Vertex3(-D1 / 2, D2 / 2, _height);

            GL.Normal3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(D1 / 2, D2 / 2, _height);
            GL.Vertex3(D1 / 2, D2 / 2, 0);
            GL.Vertex3(-D1 / 2, D2 / 2, 0);
            GL.Vertex3(-D1 / 2, D2 / 2, _height);

            GL.Normal3(0.0f, -1.0f, 0.0f);
            GL.Vertex3(D1 / 2, -D2 / 2, _height);
            GL.Vertex3(D1 / 2, -D2 / 2, 0);
            GL.Vertex3(-D1 / 2, -D2 / 2, 0);
            GL.Vertex3(-D1 / 2, -D2 / 2, _height);

            GL.Normal3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(D1 / 2, D2 / 2, _height);
            GL.Vertex3(D1 / 2, -D2 / 2, _height);
            GL.Vertex3(-D1 / 2, -D2 / 2, _height);
            GL.Vertex3(-D1 / 2, D2 / 2, _height);


            GL.Normal3(0.0f, 0.0f, -1.0f);
            GL.Vertex3(D1 / 2, D2 / 2, 0);
            GL.Vertex3(D1 / 2, -D2 / 2, 0);
            GL.Vertex3(-D1 / 2, -D2 / 2, 0);
            GL.Vertex3(-D1 / 2, D2 / 2, 0);

            GL.End();
            base.DrawSchema();
        }
    }
}

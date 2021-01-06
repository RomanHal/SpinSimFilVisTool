using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace GraphicLibrary
{
    class Tetrahedron:Object3d
    {

        public Tetrahedron(Point firstPoint, Point lastPoint, Color color,double radius) : base(firstPoint,lastPoint,radius,null,color, FigureType.Tetrahedron)
        {
            D1= radius;
            D2 = D1;
        }
       

        protected override void DrawSchema()
        {
            var height = (_firstPoint-_lastPoint).Length;
          //  if (_firstPoint.Length > _lastPoint.Length) height *= -1;

            var x = Math.Cos(0) * D1;
            var y = Math.Sin(0) * D1;
            var xx = Math.Cos(Math.PI / 3 * 2) * D1;
            var yy = Math.Sin(Math.PI / 3 * 2) * D1;
            var xxx = Math.Cos(Math.PI / 3 * 4) * D1;
            var yyy = Math.Sin(Math.PI / 3 * 4) * D1;

            GL.Begin(PrimitiveType.Triangles);
            // GL.Normal3(1.0f, 0.0f, 0.0f);
            GL.Color3(Color);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(x, y, height);
            GL.Vertex3(xx, yy, height);
            
            GL.Vertex3(0, 0,0);
            GL.Vertex3(x, y, height);
            GL.Vertex3(xxx, yyy, height);
            
            GL.Vertex3(0, 0,0);
            GL.Vertex3(xx, yy, height);
            GL.Vertex3(xxx, yyy, height);

            GL.End();


        }


    }



}

using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GraphicLibrary
{
    class Cylinder :ClickableObject
    {

        public Cylinder(Point firstPoint, Point lastPoint, double radius, Color color)
            : base(firstPoint,lastPoint, radius,null,color,FigureType.Cylinder )
        {
        }

        protected override void DrawSchema()
        {
            var height = (_lastPoint - _firstPoint).Length;
            int stacks = 1000 ;
            
            for (int i = 0; i < stacks; i++)
            {
                double rotor = 2 * Math.PI / stacks * i;
                double rotor2 = 2 * Math.PI / stacks * (i + 1);
               
                
                double upper = Math.PI / stacks * i;
                double upperHeight = Math.Cos(upper);
                double upperWidth = Math.Sin(upper);

                double lower = Math.PI / stacks * (i + 1);
                double lowerHeight = Math.Cos(lower);
                double lowerWidth = Math.Sin(lower);

                double x = Math.Cos(rotor) * D1/2;
                double x1 = Math.Cos(rotor2) * D1/2;
                double y = Math.Sin(rotor) * D2/2;
                double y1 = Math.Sin(rotor2) * D2/2;

                GL.Begin(PrimitiveType.Triangles);
                GL.Color3(Color);
                GL.Normal3(0, 0, -1);
                GL.Vertex3(x, y, height);
                GL.Vertex3(x1, y1, height);
                GL.Vertex3(0, 0, height);

                GL.Normal3(0, 1, 0);
                GL.Vertex3(x, y, 0);
                GL.Vertex3(x1, y1, 0);
                GL.Vertex3(0, 0, 0);

                GL.End();

                GL.Begin(PrimitiveType.Quads);
                GL.Normal3(x * lowerWidth, lowerHeight, y * lowerWidth);

                GL.Vertex3(x, y,0);//r * y * lowerWidth
                GL.Vertex3(x, y, height );
                GL.Vertex3(x1, y1,height);
                GL.Vertex3(x1, y1, 0);//r * y * upperWidth

                GL.End();
               
            }
            base.DrawSchema();
        }

    }
}

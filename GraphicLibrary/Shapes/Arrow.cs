using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GraphicLibrary
{
    class Arrow : ClickableObject
    {
        Tetrahedron _tetrahedron;
        Cylinder _cylinder;
        public Arrow(Point firstPoint, Point lastPoint,double d1, Color color ) : base( firstPoint,lastPoint,d1,null,color, FigureType.Arrow)
        {
            CreateArrow();
            DimensionChanged += Arrow_DimensionChanged;
        }

        private void Arrow_DimensionChanged(object sender, EventArgs e)
        {
            CreateArrow();
        }
        void CreateArrow()
        {
            IsComplexShape = true;
            var vector = _lastPoint - _firstPoint;
            var length = (_firstPoint - _lastPoint).Length;
            //D1 = D2 = length * 0.1;
            vector.Normalize();
            _tetrahedron = new Tetrahedron(Point.GetPoint(_firstPoint), 
                Point.GetPoint(_firstPoint + (vector * length * 0.3)), Color, D1);
            _cylinder = new Cylinder(Point.GetPoint(_firstPoint + (vector * length * 0.3)), 
                Point.GetPoint(_lastPoint), D1 / 2, Color);
        }

        protected override void DrawSchema()
        {
            _cylinder.Draw();
            _tetrahedron.Draw();
        }
    }
}

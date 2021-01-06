using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GraphicLibrary
{
    class DoubleArrow : ClickableObject
    {
        Arrow _firstArrow, _lastArrow;
        public DoubleArrow(Point firstPoint,Point lastPoint, Color color) : base(firstPoint,lastPoint,0,null,color,FigureType.DoubleArrow)
        {
            IsComplexShape = true;
            Vector3d vector = Point.GetVector3(lastPoint) - Point.GetVector3(firstPoint);
            D1 = D2 = vector.Length*0.2;
            
            vector *= 0.5;
            vector += Point.GetVector3(firstPoint);
            _firstArrow = new Arrow(firstPoint,Point.GetPoint( vector),D1, color);
            _lastArrow = new Arrow(lastPoint, Point.GetPoint(vector), D1, color);
            DimensionChanged += DoubleArrow_DimensionChanged;
        }
        private void Create()
        {
            Vector3d vector = _lastPoint - _firstPoint;
            vector *= 0.5;
            vector += _firstPoint;
            _firstArrow = new Arrow(Point.GetPoint(_firstPoint), Point.GetPoint(vector),D1, Color);
            _lastArrow = new Arrow(Point.GetPoint(_lastPoint), Point.GetPoint(vector),D1, Color);
        }

        private void DoubleArrow_DimensionChanged(object sender, EventArgs e)
        {
            Create();
        }

        protected override void DrawSchema()
        {
            _firstArrow.Draw();
            _lastArrow.Draw();
        }
    }
}

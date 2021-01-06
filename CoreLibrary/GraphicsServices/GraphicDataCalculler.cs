using GraphicLibrary;
using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.GraphicsServices
{
    class GraphicDataCalculler
    {
        public Tuple<Point,Point,double,double>Calculate(Range x,Range y, Range z,bool hasScript,double normX=1,
            double normY=1,double normZ=1)
        {
            if (!hasScript) return CreateShapeData(z, x, y, normX, normY, normZ);
            var length = Math.Min(x.Length, Math.Min(y.Length, z.Length));
            switch(length)
            {
                case double i when i == x.Length:return CreateShapeData(x, z, y,normX,normY,normZ);
                case double i when i == y.Length:return CreateShapeData(y, x, z, normY, normX, normZ);
                case double i when i == z.Length:return CreateShapeData(z, x, y, normZ, normX, normY);
                default:return CreateShapeData(z, x, y, normX, normY, normZ);
            }
        }
        public Tuple<Point, Point, double, double> Calculate(Range x, Range y, Range z, bool hasScript, double normX,
            double normY, double normZ, double norm)
        {
            var tuple = Calculate(x, y, z, hasScript, normX, normY, normZ);
            tuple = new Tuple<Point, Point, double, double>(tuple.Item1 * norm, tuple.Item2 * norm, 
                tuple.Item3 * norm, tuple.Item4 * norm);
            return tuple;
        }

        private Tuple<Point, Point, double, double> CreateShapeData(Range axisRange, Range d1Range, Range d2Range,
            double normAxis, double normD1, double normD2)
        {
            var d1Average = (d1Range.Max + d1Range.Min) / 2;
            var d2Average = (d2Range.Max + d2Range.Min) / 2;

            var firstPoint = new Point(d1Average* normD1, d2Average* normD2, axisRange.Min * normAxis);
            var lastPoint = new Point(d1Average * normD1, d2Average * normD2, axisRange.Max * normAxis);
            var d1 = (d1Range.Max - d1Range.Min)*normD1;
            var d2 = (d2Range.Max - d2Range.Min)*normD2;
            return new Tuple<Point, Point, double, double>(firstPoint, lastPoint, d1, d2);
        }

        private void CreateCuboid(out Point firstPoint,out Point lastPoint,out double d1,
            out double d2,Range x,Range y,Range z)
        {
            firstPoint = new Point((x.Max + x.Min) / 2, (y.Max + y.Min) / 2, z.Min);
            lastPoint = new Point((x.Max + x.Min) / 2, (y.Max + y.Min) / 2, z.Max);
            d1 = x.Max - x.Min;
            d2 = y.Max - y.Min;
        }

        private void CreateEllispeZData(out Point firstPoint, out Point lastPoint, 
            out double d1, out double d2, Range x, Range y, Range z)
        {
            firstPoint = new Point((x.Min + x.Max) / 2, (y.Min + y.Max) / 2, z.Min);
            lastPoint = new Point((x.Min + x.Max) / 2, (y.Min + y.Max) / 2, z.Max);
            d2 = x.Max - x.Min;
            d1 = y.Max - y.Min;
        }

        private void CreateEllispeYData(out Point firstPoint, out Point lastPoint, 
            out double d1, out double d2, Range x, Range y, Range z)
        {
            firstPoint = new Point((x.Min + x.Max) / 2,
                y.Min, (z.Min + z.Max) / 2);
            lastPoint = new Point((x.Min + x.Max) / 2,
                y.Max, (z.Min + z.Max) / 2);
            d2 = z.Max - z.Min;
            d1 = x.Max - x.Min;
        }

        private void CreateEllispeXData(out Point firstPoint, out Point lastPoint, 
            out double d1, out double d2, Range x, Range y, Range z)
        {
            firstPoint = new Point(x.Min, (y.Min + y.Max)
                / 2, (z.Min + z.Max) / 2);
            lastPoint = new Point(x.Max, (y.Min + y.Max)
                / 2, (z.Min + z.Max) / 2);
            d2 = z.Max - z.Min;
            d1 = y.Max - y.Min;
        }
    }
}

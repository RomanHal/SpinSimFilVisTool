using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Parts
{
    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Length=> Math.Sqrt(X * X + Y * Y + Z * Z);
        public Vector() { }
        public Vector(Vector vector):this(vector.X,vector.Y,vector.Z) { }
        public Vector(double x,double y,double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector  Normalized()
        {
            return new Vector(this * (1 / Length));
        }

        public static Vector operator*(Vector vector,double value)
        {
            return new Vector(vector.X * value, vector.Y * value, vector.Z * value);
        }
        public override string ToString()
        {
            return X+" "+Y+" "+Z;
        }
    }
}

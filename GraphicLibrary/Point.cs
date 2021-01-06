
namespace GraphicLibrary
{
    using OpenTK;
    using System;

    public class Point
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double Length { get => Math.Sqrt(X * X + Y * Y + Z * Z); }

    internal static Vector3d GetVector3(Point point )
    {
        return new Vector3d(point.X, point.Y, point.Z);
    }
    public Point() { }
    public Point(double x, double y, double z)
    {
            Z = z;
            Y = y;
            X = x;
    }
    
    public static Point GetPoint(Vector3d vector3)
    {
        return new Point() { X = vector3.X, Y = vector3.Y, Z = vector3.Z };
    }

    public static Point GetPoint(double x,double y, double z)
    {
        return new Point() { X = x, Y = y, Z = z };
    }
        
    public static Point operator*(Point point,double multiplier)
        {
            return new Point(point.X * multiplier, point.Y * multiplier, point.Z * multiplier);
        }
        public static Point operator+(Point first,Point second)
        {
            return new Point(first.X + second.X, first.Y + second.Y, first.Z + second.Z);
        }
}

}
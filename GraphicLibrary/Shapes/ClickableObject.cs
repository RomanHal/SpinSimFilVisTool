using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GraphicLibrary
{
    abstract class  ClickableObject : Object3d
    {
        Vector3d[] _points;
        Vector3d[] _actualPoints;

        
        public ClickableObject(Point firstPoint,Point lastPoint,double d1,double? d2, Color color,FigureType type)
            :base(firstPoint,lastPoint,d1,d2,color,type)
        {
            UpdateCage();
            DimensionChanged += OnDimensionChange;
        }

        private void OnDimensionChange(object sender, EventArgs e)
        {
            UpdateCage();
        }

        private void UpdateCage()
        {
            _points = new Vector3d[] { new Vector3d(D1/2,  D2/2, 0),
                                new Vector3d(-D1/2, D2/2, 0),
                                new Vector3d(-D1/2, -D2/2, 0),
                                new Vector3d(D1/2, -D2/2, 0),
                                new Vector3d(D1/2,  D2/2,  (_lastPoint-_firstPoint).Length),
                                new Vector3d(-D1/2,  D2/2,  (_lastPoint-_firstPoint).Length),
                                new Vector3d(-D1/2,  -D2/2,  (_lastPoint-_firstPoint).Length),
                                new Vector3d(D1/2,  -D2/2, (_lastPoint-_firstPoint).Length)};
            _actualPoints = (Vector3d[])_points.Clone();
            for (int i = 0; i < _points.Length; i++)
            {
                _actualPoints[i] = Vector3d.Transform(_points[i], _transformationMatrix);
            }
        }
                
        protected override void DrawSchema()
        {

            //GL.Begin(PrimitiveType.Points);
            //GL.Color3(Color.Red);
            //foreach (var point in _actualPoints)
            //{
            //    GL.Vertex3(point);
            //}
            //GL.End();
        }

        public bool PointBelongsTo(Point point)
        {
            return PointBelongsTo(Point.GetVector3(point));
        }
        public bool PointBelongsTo(Vector3d point)
        {
            if (!IsBetweenPlanes(_actualPoints[0], _actualPoints[1], point)) return false;
            if (!IsBetweenPlanes(_actualPoints[0], _actualPoints[3], point)) return false;
            if (!IsBetweenPlanes(_actualPoints[0], _actualPoints[4], point)) return false;
            return true;
        }
        public bool PointBelongsTo(Vector3 point)
        {
            return PointBelongsTo((Vector3d)point);
        }
        
        public double GetDistanceFrom(Point point)
        {
            return GetDistanceFrom(Point.GetVector3(point));
        }
        public double GetDistanceFrom(Vector3 point)
        {
            return GetDistanceFrom((Vector3d)point);
        }

        public double GetDistanceFrom(Vector3d point)
        {
            Vector3d avarage = Vector3d.Zero;
            for (int i = 0; i < _actualPoints.Length; i++)
            {
                avarage += _actualPoints[i];
            }
            avarage /= _actualPoints.Length;
            return (point - avarage).Length;
        }

        bool IsBetweenPlanes(Vector3d point1,Vector3d point2,Vector3d pointToCheck )
        {
            double distanceBetweenPlanes = (point1 - point2).Length;
            Vector3d normal = (point2 - point1).Normalized();
            Vector4d plane = new Vector4d
            {
                X = normal.X,
                Y = normal.Y,
                Z = normal.Z,
                W = -(normal.X * point1.X + normal.Y * point1.Y + normal.Z * point1.Z)
            };
            double distancePlane1ToPoint = Math.Abs(pointToCheck.X * plane.X + pointToCheck.Y * plane.Y 
                + pointToCheck.Z * plane.Z + plane.W) / normal.Length;
            normal = (point1 - point2).Normalized();
            plane = new Vector4d
            {
                X = normal.X,
                Y = normal.Y,
                Z = normal.Z,
                W = -(normal.X * point2.X + normal.Y * point2.Y + normal.Z * point2.Z)
            };
            double distancePlane2ToPoint = Math.Abs(pointToCheck.X * plane.X + pointToCheck.Y * plane.Y 
                + pointToCheck.Z * plane.Z + plane.W) / normal.Length;
            if (distanceBetweenPlanes * 1.01 > distancePlane1ToPoint + distancePlane2ToPoint) return true;
            return false;
        }



    }
}

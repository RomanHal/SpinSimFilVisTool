using GraphicLibrary.Interfaces;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace GraphicLibrary
{
    internal abstract class Object3d: IGraphicsObject
    {
        public string Id { get => Name;
            set => Name = value; }//TODO
        public double D1 { get=>_d1; set { _d1 = value; DimensionChanged?.Invoke(this, null); } }
        double _d1;
        public double D2 { get=>_d2; set { _d2 = value; DimensionChanged?.Invoke(this, null); } }
        double _d2;
        internal bool IsComplexShape {private get; set; }
        public Point FirstPoint {
            get =>Point.GetPoint(_firstPoint);
            set {
                _firstPoint = Point.GetVector3(value);
                DimensionChanged?.Invoke(this, null);
            }
        }
        protected Vector3d _firstPoint;
        public Point LastPoint {
            get =>Point.GetPoint(_lastPoint);
            set {
                _lastPoint = Point.GetVector3(value);
                DimensionChanged?.Invoke(this, null);
            }
        }
        protected Vector3d _lastPoint;
        public string Name { get; set; }
        public FigureType Type { get; set; }
        public Color Color { get; set; }
        public bool Visible { get; set; }
        protected Matrix4d _transformationMatrix;

        protected event EventHandler DimensionChanged;


        public Object3d(Color color,FigureType type)
        {
            Color = color;
            Type = type;
            Visible = true;
        }

        public Object3d(Point firstPoint,Point lastPoint,double d1,double? d2, Color color, FigureType type)
        {
            DimensionChanged += CreateTransformationMatrix;
            D1 = d1;
            if (d2.HasValue)
            {
                D2 = d2.Value;
            }
            else
            {
                D2 = d1;
            }
            _firstPoint = Point.GetVector3(firstPoint);
            _lastPoint = Point.GetVector3(lastPoint);
            Color = color;
            Type = type;
            Visible = true;
            CreateTransformationMatrix();
        }

        private void CreateTransformationMatrix(object sender, EventArgs e)
        {
            CreateTransformationMatrix();
        }

        public void Draw()
        {
            if(Visible==true)
            {
                GL.PushMatrix();
                if(!IsComplexShape) GL.MultMatrix(ref _transformationMatrix);
                DrawSchema();
                GL.PopMatrix();
            }
        }
        protected abstract void DrawSchema();      
        public override string ToString()
        {
            return Name;
        }        
        void CreateTransformationMatrix()
        {
            var direction = _lastPoint - _firstPoint;
            double fi = Math.Acos(direction.Z / direction.Length);
            double alpha = Math.Atan2(direction.Y, direction.X)+Math.PI/2;
            var transformationMatrix = Matrix4d.CreateRotationX(fi);
            transformationMatrix *= Matrix4d.CreateRotationZ(alpha);
            transformationMatrix *= Matrix4d.CreateTranslation(_firstPoint);
            _transformationMatrix = transformationMatrix;
        }

        Matrix4d CreateRotationY(Vector3d position, Vector3d target)
        {
            var angle= Vector3d.CalculateAngle(new Vector3d(position.X, 0, position.Z), new Vector3d(0, 0, 1));
            return Matrix4d.CreateFromAxisAngle(Vector3d.UnitY, angle);
        }

        Matrix4d CreateRotationX(Vector3d position,Vector3d target)
        {
            var angle = Vector3d.CalculateAngle(new Vector3d(0, position.Y, 0), new Vector3d( 0, 0, 1));
            return Matrix4d.CreateFromAxisAngle(Vector3d.UnitX, angle);
        }
       
    }

}

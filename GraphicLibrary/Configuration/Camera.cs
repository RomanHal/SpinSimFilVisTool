using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Reactive.Linq;

namespace GraphicLibrary.Configuration
{

    class Camera
    {
        public Vector3 Direction { get => (_position - _direction).Normalized(); }
        public Vector3 Position { get => (_position-_direction).Normalized()*
                ((_position - _direction).Length-1)+_direction; }
        public Vector3 UpVersor { get => _up; }
        Vector3 _position;
        Vector3 _direction;
        Vector3 _up;
        int _width;
        int _height;
        double _fi, _theta;
        float _distance = 4;
        MouseDevice _mouse;
        Matrix4 _modelViewMatrix;
        Matrix4 _projectionMatrix;
        IObservable<MouseMoveEventArgs> MouseMove { get; set; }
        IObservable<MouseButtonEventArgs> MouseRightDown { get; set; }
        IObservable<MouseButtonEventArgs> MouseRightUp { get; set; }

        public Camera(MouseDevice mouse,int width,int height)
        {
            _height = height;
            _width = width;
            _mouse = mouse;
            MouseMove = Observable.FromEvent<EventHandler<MouseMoveEventArgs>, MouseMoveEventArgs>(
                h => (s, e) => h(e),
                h => mouse.Move+= h,
                h => mouse.Move-= h);

            MouseRightDown = Observable.FromEvent<EventHandler<MouseButtonEventArgs>, MouseButtonEventArgs>(
                h => (s, e) => h(e),
                h =>mouse.ButtonDown+=h,
                h=>mouse.ButtonDown-=h);
            MouseRightUp = Observable.FromEvent<EventHandler<MouseButtonEventArgs>, MouseButtonEventArgs>(
                h => (s, e) => h(e),
                h => mouse.ButtonUp += h,
                h => mouse.ButtonUp -= h);

            MouseMove.SkipUntil(MouseRightDown.Where(e => e.Button == MouseButton.Right))
             .TakeUntil(MouseRightUp.Where(e => e.Button == MouseButton.Right))
             .Repeat()
             .Subscribe(e =>
             {
                 Rotate(e.XDelta / 180.0 * Math.PI, e.YDelta / 180.0 * Math.PI);
             });
            _fi = 0;
            _theta = 0;
            _position = new Vector3(0, 0, _distance);
            _direction = Vector3.Zero;
            _up = Vector3.UnitY;
            _modelViewMatrix = Matrix4.LookAt(_position, _direction, _up);
        }

        public void OnWindowResize(int height,int width)
        {
            if (height != 0 && width != 0) 
            {
                _height = height;
                _width = width;
                _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, _width / _height, 1.0f, 64.0f);
            }
        }

        void Rotate(double XDelta,double YDelta)
        {
            double r =_distance;
            _fi +=YDelta;
            _theta += XDelta;
            _theta += 2 * Math.PI;
            _theta %= (2 * Math.PI);
            float w =(float) Math.Cos(_theta/ 2);
            double x =_direction.X +  r* Math.Cos(_fi) * Math.Cos(_theta);
            double y =_direction.Y + r* Math.Cos(_fi) * Math.Sin(_theta);
            double z =_direction.Z + r*  Math.Sin(_fi);
            _up = Vector3.Transform(Vector3.UnitY, new Quaternion( 0,  0, (float)Math.Sin(_theta / 2), w));
            _position =new Vector3((float)x, (float)y, (float)z);
            _modelViewMatrix = Matrix4.LookAt(_position, _direction, _up);//_up
        }
        public void Reset()
        {
            _fi =_theta= 0;
            _direction = Vector3.Zero;
            _position = new Vector3(0, 0,_distance);
            _up = Vector3.UnitY;
        }

        public Vector3 RaycastDirection()
        {
            return (MouseToWorldCoords() - _position).Normalized();
        }

        void MoveBy(Vector3 road)
        {
            _position += road;
            _direction += road;
        }
        public void MoveTo(Vector3 position)
        {
            _position = position + new Vector3(0, 0, _distance);
            _direction = position ;
        }

        public void Update()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref _modelViewMatrix);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref _projectionMatrix);
        }

        public Vector3 MouseToWorldCoords( )
        {
            double x= ( (double)_mouse.X - (double)_width / 2) / (double)_width * 0.8;
            double y = ((double)_mouse.Y - (double)_height / 2) / (double)_height * 0.8;
            var length = Math.Sqrt(x * x + y * y);
            Vector3 Versor3 = Vector3.Cross(Direction.Normalized(), UpVersor);
            float angle = (float)Math.Atan2(x, y);
            float px = Position.X + (float)length * (float)Math.Cos(angle) * UpVersor.X * -1 
                + (float)length * (float)Math.Sin(angle) * Versor3.X * -1;
            float py = Position.Y + (float)length * (float)Math.Cos(angle) * UpVersor.Y * -1 
                + (float)length * (float)Math.Sin(angle) * Versor3.Y * -1;
            float pz = Position.Z + (float)length * (float)Math.Cos(angle) * UpVersor.Z * -1 
                + (float)length * (float)Math.Sin(angle) * Versor3.Z * -1;
            return new Vector3(px, py, pz);
        }
    }
}

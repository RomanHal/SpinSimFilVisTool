using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using GraphicLibrary.Interfaces;
using GraphicLibrary.Management;
using System.ComponentModel;
using OpenTK.Input;
using System.Reactive.Linq;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GraphicLibrary.Tests")]

namespace GraphicLibrary
{

    public class MyWindow : GameWindow,IWindow
    {
        IObservable<MouseMoveEventArgs> MouseMove { get; set; }
        IObservable<MouseButtonEventArgs> MouseRightDown { get; set; }
        IObservable<MouseButtonEventArgs> MouseRightUp { get; set; }

        public List<IGraphicsObject> RenderedObjects{ private get; set;}
        readonly Configuration.Light light = new Configuration.Light();
        readonly Configuration.Camera _camera;
        readonly SelectObjectEvent _selectObjectEvent;
        readonly Color4 materialAmbient = new Color4(0.24725f, 0.1995f, 0.0225f, 1.0f);
       // readonly Color4 materialDiffuse = new Color4(0.75164f, 0.60648f, 0.22648f, 1.0f);
       // readonly Color4 materialSpecular = new Color4(0.628281f, 0.555802f, 0.366065f, 1.0f);
        

        Matrix4 rotate = Matrix4.Identity;
        float zoom = 1.0f;

        
        public int _height;
        public int _width;
        private object vector3;

        public MyWindow(int width, int height, string title)
    : base(width, height,GraphicsMode.Default,title)
        {
            InitializeComponent();
            RenderedObjects = FigureContainer.GetElements();
            _camera = new Configuration.Camera(Mouse, _width, _height);
            _selectObjectEvent = new SelectObjectEvent(RenderedObjects, Mouse, _camera);
            _selectObjectEvent.OnSelectionChange += _selectObjectEvent_OnSelectionChange;
            GraphicManagement.cameraCommands.MoveToCommand += CameraCommands_MoveToCommand;
            //Tracking

            MouseMove = Observable.FromEvent<EventHandler<MouseMoveEventArgs>, MouseMoveEventArgs>(
                h => (s, e) => h(e),
                h => Mouse.Move += h,
                h => Mouse.Move -= h);

            MouseRightDown = Observable.FromEvent<EventHandler<MouseButtonEventArgs>, MouseButtonEventArgs>(
                h => (s, e) => h(e),
                h => Mouse.ButtonDown += h,
                h => Mouse.ButtonDown -= h);
            MouseRightUp = Observable.FromEvent<EventHandler<MouseButtonEventArgs>, MouseButtonEventArgs>(
                h => (s, e) => h(e),
                h => Mouse.ButtonUp += h,
                h => Mouse.ButtonUp -= h);
            
        }

        private void CameraCommands_MoveToCommand(object sender, CameraCommandsEventArgs e)
        {
            _camera.MoveTo(new Vector3((float)e.X, (float)e.Y, (float)e.Z));
        }

        public MyWindow(int width, int height,ref double obj_height,ref double obj_width)
            : base(width, height)
        {
            InitializeComponent();
            //_height = obj_height;
            //_radius = obj_width;
            RenderedObjects = FigureContainer.GetElements();
            _camera = new Configuration.Camera(Mouse, width, height);         
            _selectObjectEvent = new SelectObjectEvent(RenderedObjects, Mouse, _camera);
            _selectObjectEvent.OnSelectionChange += _selectObjectEvent_OnSelectionChange;
            //Tracking

            MouseMove = Observable.FromEvent<EventHandler<MouseMoveEventArgs>, MouseMoveEventArgs>(
                h => (s, e) => h(e),
                h => Mouse.Move += h,
                h => Mouse.Move -= h);

            MouseRightDown = Observable.FromEvent<EventHandler<MouseButtonEventArgs>, MouseButtonEventArgs>(
                h => (s, e) => h(e),
                h => Mouse.ButtonDown += h,
                h => Mouse.ButtonDown -= h);
            MouseRightUp = Observable.FromEvent<EventHandler<MouseButtonEventArgs>, MouseButtonEventArgs>(
                h => (s, e) => h(e),
                h => Mouse.ButtonUp += h,
                h => Mouse.ButtonUp -= h);

        }

        private void _selectObjectEvent_OnSelectionChange(object sender, SelectObjectEventArgs e)
        {
            SelectedObjectManagement.OnClickSelectMethod(e.SelectedObject);
        }

        void InitializeComponent()
        {
       
        }

        void PrintScreen(object sender,PrintScreenEventArgs eventsArgs)
        {
            try
            {
                int border = (Bounds.Right - Bounds.Left - Width) / 2;
                int titleborder = Bounds.Bottom - Bounds.Top - Height - border;
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.Width, this.Height);
                System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
                graphics.CopyFromScreen(new System.Drawing.Point() { X = Location.X + border, Y = Location.Y + titleborder },
                    new System.Drawing.Point() { X = this.ClientRectangle.Left, Y = ClientRectangle.Top },
                    new System.Drawing.Size(this.Size.Width, this.Size.Height));
                bitmap.Save(eventsArgs.Path + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch(ObjectDisposedException)
            {
                PrintScreenMaker.PrintScreenCommand -= PrintScreen;
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            PrintScreenMaker.PrintScreenCommand += PrintScreen;
            FigureContainer.closeWindow.CloseWindowCommand += CloseWindowCommand;
            FigureContainer.moveWindow.MoveWindowsCommand += MoveWindowsCommand;
            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);
            
            light.SetLights();

            //Normalization
            GL.Enable(EnableCap.Normalize);

            GL.Enable(EnableCap.ColorMaterial);
            GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.Emission);



            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, materialAmbient);
        }

        private void MoveWindowsCommand(object sender, MoveWindowEventArgs e)
        {
            Location = new OpenTK.Point(e.X, e.Y);
        }

        private void CloseWindowCommand(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(ClientRectangle);
            _camera.OnWindowResize(Height, Width);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            PrintScreenMaker.PrintScreenCommand -= PrintScreen;
            FigureContainer.closeWindow.CloseWindowCommand -= CloseWindowCommand;
            FigureContainer.moveWindow.MoveWindowsCommand -= MoveWindowsCommand;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _camera.Update();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            
            RenderedObjects.Where(s => s is Object3d).Select(s => (Object3d)s)?.ToList()?.ForEach((s)
                => s.Draw());
 
            GL.PopMatrix();
            SwapBuffers();
        }
        
        void DrawGrid()
        {
            GL.Begin(PrimitiveType.Lines);
            for(int k=-10;k<=10;k++)
            for(int j=-10;j<=10;j++)
            for(int i=-10; i<=10;i++)
            {
                GL.Color3(Color.White);
                GL.Vertex3(i, k, j);
                GL.Vertex3(i, k+1, j);
                        GL.Vertex3(i, k, j);
                GL.Vertex3(i + 1, k, j);
                        GL.Vertex3(i, k, j);
                GL.Vertex3(i, k, j + 1);
            }

            GL.End();
        }
    }
}

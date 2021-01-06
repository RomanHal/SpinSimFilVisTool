using CoreLibrary.GraphicsServices;
using CoreLibrary.Interfaces;
using GraphicLibrary;
using GraphicLibrary.Interfaces;
using MifParser.Interfaces;

namespace CoreLibrary
{
    class EnergyPart : IEnergyPart
    {
        internal IGraphicsObject _graphicsObject;
        public string Region { get ; set ; }
        Point FirstPoint { get => _graphicsObject.FirstPoint; set => 
                _graphicsObject.FirstPoint = value; }
        Point LastPoint { get => _graphicsObject.LastPoint; set => 
                _graphicsObject.LastPoint = value; }
        public double DirectionX { get=>FirstPoint.X-LastPoint.X;
            set =>FirstPoint=new Point(value+LastPoint.X,FirstPoint.Y,FirstPoint.Z); }
        public double DirectionY { get => FirstPoint.Y - LastPoint.Y;
            set => FirstPoint =new Point(FirstPoint.X,value+LastPoint.Y,FirstPoint.Z) ; }
        public double DirectionZ { get => FirstPoint.Z - LastPoint.Z;
            set => FirstPoint= new Point(FirstPoint.X, FirstPoint.Y, value+LastPoint.Z); }
        public double PositionX { get => LastPoint.X; set
            {
                LastPoint = new Point(value, LastPoint.Y, LastPoint.Z);
                FirstPoint =FirstPoint+LastPoint;
            } }
        public double PositionY { get => LastPoint.Y; set {
                LastPoint = new Point(LastPoint.X, value, LastPoint.Z);
                FirstPoint = FirstPoint + LastPoint;
            } }
        public double PositionZ { get => LastPoint.Z; set
            {
                LastPoint = new Point(LastPoint.X, LastPoint.Y, value);
                FirstPoint = FirstPoint + LastPoint;
            } }
        public double Value { get; set; }
        public IColor Color { get=>new CoreColor(_graphicsObject.Color);
            set => _graphicsObject.Color= GetOpenTkColor(value); }
        public bool Visible { get=>_graphicsObject.Visible; set=>_graphicsObject.Visible=value; }
        public EnergyPart(IGraphicsObject graphicsObject,string region,double value)
        {
            Region = region;
            _graphicsObject = graphicsObject;
            Value = value;
        }
        OpenTK.Color GetOpenTkColor(IColor color)
        {
            return new OpenTK.Color(color.R, color.G, color.B, 255);
        }
    }
}

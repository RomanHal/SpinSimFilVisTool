using CoreLibrary.Enums;
using CoreLibrary.GraphicsServices;
using CoreLibrary.Interfaces;
using CoreLibrary.Positioners;
using GraphicLibrary;
using GraphicLibrary.Interfaces;
using MifParser.Atlases;
using MifParser.Interfaces;
using System;
using System.Collections.Generic;

namespace CoreLibrary
{
    class SpintronicsObject:IObject
    {
        public static int amount;
        IGraphicsObject _graphicObject;
        IMifAtlas _mifObject;
        public AtlasType Type => GetAtlasType();
        private string _name;
        private string _multiAtlas;
        private IScript _script;
        private FigureType _figureType;
        private GraphicDataCalculler _graphicCalculler = new GraphicDataCalculler();
        internal IGraphicsObject GraphicsObject { get => _graphicObject; }
        internal IMifAtlas MifObject { get => _mifObject; }


        public SpintronicsObject(IMifAtlas atlas, IGraphicsObject graphics,string multiAtlas=null)
        {
            _mifObject = atlas;
            _graphicObject = graphics;
            _multiAtlas = multiAtlas;
            if (!double.IsPositiveInfinity(XyzConstrainsModifier.Norm) || !double.IsNegativeInfinity(XyzConstrainsModifier.Norm))
                XyzConstrainsModifier.Norm = 1/XyzConstrainsModifier.Norm * amount + GetAverageLength() / (amount + 1);
            else XyzConstrainsModifier.Norm = GetAverageLength();
            amount++;
        }
        ~SpintronicsObject()
        {
            XyzConstrainsModifier.Norm = ((XyzConstrainsModifier.Norm * amount - GetAverageLength()) / (amount - 1));
            amount--;
        }

        private AtlasType GetAtlasType()
        {
            switch(_mifObject.GetType())
            {
                case Type T when T == typeof(BoxAtlas):
                    return AtlasType.BoxAtlas;
                case Type T when T == typeof(ScriptAtlas):
                    return AtlasType.ScriptAtlas;
                case Type T when T == typeof(MultiAtlas):
                    return AtlasType.MultiAtlas;
                default:throw new NotSupportedException(_mifObject.GetType() + " is not supported.");
            }
        }
        public void Refresh() => RecalculateGraphic();
        private void RecalculateGraphic()
        {
            //_graphicCalculler.Calculate( out Point firstPoint, out Point lastPoint, out double d1,
            //    out double d2, _mifObject.X, _mifObject.Y, _mifObject.Z, Script?.Name,1/XyzConstrinsModifier.Norm);
            //_graphicObject.FirstPoint = firstPoint;
            //_graphicObject.LastPoint = lastPoint;
            //_graphicObject.D1 = d1;
            //_graphicObject.D2 = d2;
            var shapeData = _graphicCalculler.Calculate(_mifObject.X, _mifObject.Y, _mifObject.Z, 
                Script != null ? true : false, XyzConstrainsModifier.XConstrin, XyzConstrainsModifier.YConstrin
                , XyzConstrainsModifier.ZConstrin, XyzConstrainsModifier.Norm);
            _graphicObject.FirstPoint = shapeData.Item1;
            _graphicObject.LastPoint = shapeData.Item2;
            _graphicObject.D1 = shapeData.Item3;
            _graphicObject.D2 = shapeData.Item4;
        }

        OpenTK.Color GetColor(IColor color)
        {
            return new OpenTK.Color(color.R, color.G, color.B, 255);
        }
        public IScript Script
        {
            get => (_mifObject is ScriptAtlas) ?
                new MifScript(((ScriptAtlas)_mifObject).Script) : null;
            set { if (_mifObject is ScriptAtlas) ((ScriptAtlas)_mifObject).Script =
                (IMifScript)value;
            }
        }
        public string ScriptArgs
        {
            get =>(_mifObject is ScriptAtlas)? ((ScriptAtlas)_mifObject).ScriptArgs:null;
            set { if (_mifObject is ScriptAtlas) ((ScriptAtlas)_mifObject).ScriptArgs = value; }
        }

        public double Xmin { get => _mifObject.X.Min; set { _mifObject.X.Min = value; RecalculateGraphic(); } }
        public double Xmax { get => _mifObject.X.Max; set { _mifObject.X.Max = value; RecalculateGraphic(); } }
        public double Ymin { get => _mifObject.Y.Min; set { _mifObject.Y.Min = value; RecalculateGraphic(); } }
        public double Ymax { get => _mifObject.Y.Max; set { _mifObject.Y.Max=value; RecalculateGraphic(); } }
        public double Zmin { get => _mifObject.Z.Min; set { _mifObject.Z.Min = value; RecalculateGraphic(); } }
        public double Zmax { get => _mifObject.Z.Max; set { _mifObject.Z.Max = value; RecalculateGraphic(); } }
        public string Multiatlas { get => _multiAtlas; set => _multiAtlas=value; }
        public string Name { get => _mifObject.Name; set { _graphicObject.Id = value;
                _mifObject.Name = value;  } }
        public bool ColorByMs { get ; set ; }
        public IColor Color { get => new CoreColor(_graphicObject.Color); set => _graphicObject.Color=GetColor(value); }
        public bool Visible { get => _graphicObject.Visible; set => _graphicObject.Visible=value; }
        public List<string> Regions { get=>_mifObject.Regions; set=>_mifObject.Regions=value ; }
        public double GetAverageLength()
        {
            return 3*3/(Math.Abs(Xmax - Xmin) + Math.Abs(Ymax - Ymin) + Math.Abs(Zmax - Zmin)) ;
        }

    }
}

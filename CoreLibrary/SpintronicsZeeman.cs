using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLibrary.Enums;
using CoreLibrary.Interfaces;
using GraphicLibrary.Interfaces;
using MifParser.Interfaces;

namespace CoreLibrary
{
    public class SpintronicsZeeman : SpintronicsEnergy
    {
        public double Multuplier { get; set; }
        public override EnergyType Type => EnergyType.UZeeman;

        internal override List<IGraphicsObject> GraphicsObjects => _parts.Select(p=>((EnergyPart)p.Item1)
            ._graphicsObject).Concat(_parts.Select(p => ((EnergyPart)p.Item1)._graphicsObject)).ToList();
        List<(IEnergyPart, IEnergyPart)> _parts = new List<(IEnergyPart, IEnergyPart)>();

        public IColor[] Colors => _parts.Select(p => p.Item1.Color).ToArray();
        public bool[] Visibility => _parts.Select(p => p.Item1.Visible).ToArray();
        public bool[] Visibility2 => _parts.Select(p => p.Item2.Visible).ToArray();
        public int[] Steps => _parts.Select(p => Convert.ToInt32(p.Item1.Value)).ToArray();
        public double[] StartVectorX => _parts.Select(p => p.Item1.DirectionX).ToArray();
        public double[] StartVectorY => _parts.Select(p => p.Item1.DirectionY).ToArray();
        public double[] StartVectorZ => _parts.Select(p => p.Item1.DirectionZ).ToArray();
        public double[] EndVectorX => _parts.Select(p => p.Item2.DirectionX).ToArray();
        public double[] EndVectorY => _parts.Select(p => p.Item2.DirectionY).ToArray();
        public double[] EndVectorZ => _parts.Select(p => p.Item2.DirectionZ).ToArray();
        public double[] PointX => _parts.Select(p => p.Item1.PositionX).ToArray();
        public double[] PointY => _parts.Select(p => p.Item1.PositionY).ToArray();
        public double[] PointZ => _parts.Select(p => p.Item1.PositionZ).ToArray();

        IEnergyPart _inQueue;

        public SpintronicsZeeman(){ }
        public SpintronicsZeeman(IMifEnergy energy,List<(IEnergyPart,IEnergyPart)> list)
        {
            _energy = energy;
            _parts = list;
        }


        public override void AddEnergyPart(IEnergyPart part)
        {
            if (_inQueue == null)
                _inQueue = part;
            else
            {
                _parts.Add((_inQueue, part));
                _inQueue = null;
            }
        }
        public IEnergyPart GetEnergyPart(int index,bool first=true)
        {
            return first ? _parts[index].Item1 : _parts[index].Item2;
        }

        public IEnergyPart GetTwin(IEnergyPart part)
        {
            var item = _parts.Where(p => p.Item1 == part || p.Item2 == part).FirstOrDefault();
            if (item.Item1 == part) return item.Item2;
            return item.Item1;
        }
        public void Change(string name, double multiplier, int index,double startVectorX, double startVectorY, double startVectorZ,double endVectorX, double endVectorY, double endVectorZ,
            double pointX, double pointY, double pointZ,int steps,IColor color, bool visible,bool visibleSecond)
        {
            Name = name;
            Multuplier = multiplier;
            _parts[index].Item1.DirectionX = startVectorX;
            _parts[index].Item1.DirectionY = startVectorY;
            _parts[index].Item1.DirectionZ = startVectorZ;
            _parts[index].Item1.PositionX = pointX;
            _parts[index].Item1.PositionY = pointY;
            _parts[index].Item1.PositionZ = pointZ;
            _parts[index].Item2.PositionX = pointX;
            _parts[index].Item2.PositionY = pointY;
            _parts[index].Item2.PositionZ = pointZ;
            _parts[index].Item2.DirectionX = endVectorX;
            _parts[index].Item2.DirectionY = endVectorY;
            _parts[index].Item2.DirectionZ = endVectorZ;

        }

        public override void RemoveEnergyPart(IEnergyPart part)
        {
            var index= _parts.Where(p => p.Item1 == part || p.Item2 == part).Select((s,i)=>i).FirstOrDefault();
            if(_parts[index].Item1==null)
            {
                if(_parts[index].Item2==part)
                {
                    _parts.RemoveAt(index);
                }
                else if(_parts[index].Item2==null)
                {
                    _parts.RemoveAt(index);
                }
                else if(_parts[index].Item2!=null)
                {
                    throw new FieldAccessException();
                }
            }
            else if(_parts[index].Item1==part)
            {
                if(_parts[index].Item2==null)
                {
                    _parts.RemoveAt(index);
                }
                else if(_parts[index].Item2!=null)
                {
                    _parts[index]=(null, _parts[index].Item2);
                }
            }
            else if(_parts[index].Item1!=null)
            {
                if (_parts[index].Item2== null) throw new FieldAccessException();
                if (_parts[index].Item2 == part)
                    _parts[index] = (_parts[index].Item1, null);

            }
        }
    }
}

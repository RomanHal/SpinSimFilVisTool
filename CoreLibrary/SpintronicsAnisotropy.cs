using CoreLibrary.Enums;
using CoreLibrary.Interfaces;
using CoreLibrary.Positioners;
using GraphicLibrary.Interfaces;
using MifParser.Energies;
using MifParser.Interfaces;
using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreLibrary
{
    public class SpintronicsAnisotropy : SpintronicsEnergy
    {
        public string Atlas { get=>((UniaxialAnisotropy)_energy).Axis.Atlas?.Name;
            set => ((UniaxialAnisotropy)_energy).Axis.Atlas.Name=value; }//Binding needed//NULL ATLAS exception
        public string[] Regions => _parts.Select(p => p.Region).ToArray();
        public double[] Values => _parts.Select(p => p.Value).ToArray();
        public bool[] Visibility => _parts.Select(p => p.Visible).ToArray();
        public IColor[] Colors => _parts.Select(p => p.Color).ToArray();
        public double[] DirectionX => _parts.Select(p => p.DirectionX).ToArray();
        public double[] DirectionY => _parts.Select(p => p.DirectionY).ToArray();
        public double[] DirectionZ => _parts.Select(p => p.DirectionZ).ToArray();
        public double[] PositionX => _parts.Select(p => p.PositionX/XyzConstrainsModifier.Norm/ XyzConstrainsModifier.XConstrin)
            .ToArray();
        public double[] PositionY => _parts.Select(p => p.PositionY / XyzConstrainsModifier.Norm / XyzConstrainsModifier.YConstrin)
            .ToArray();
        public double[] PositionZ => _parts.Select(p => p.PositionZ / XyzConstrainsModifier.Norm / XyzConstrainsModifier.ZConstrin)
            .ToArray();
        public override EnergyType Type => GetEnergyType();
        internal override List<IGraphicsObject> GraphicsObjects => _parts.Select
            (p=>((EnergyPart)p)._graphicsObject).ToList();
        List<IEnergyPart> _parts = new List<IEnergyPart>();

        public SpintronicsAnisotropy(IMifEnergy energy,List<IEnergyPart> energies)
        {
            _energy = energy;
            _parts = energies;
        }

        EnergyType GetEnergyType()
        {
            switch(_energy.GetType())
            {
                case Type T when T == typeof(UniaxialAnisotropy):
                    return EnergyType.UniaxialAnisotropy;

                default: throw new NotSupportedException();
            }
        }
        public override void AddEnergyPart(IEnergyPart energy)
        {
            _parts.Add(energy);
            Vector vector = new Vector(energy.DirectionX, energy.DirectionY, energy.DirectionZ);
            ((UniaxialAnisotropy)_energy).Axis.RegionsValue.Add((energy.Region, vector));
            ((UniaxialAnisotropy)_energy).K1.RegionsValue.Add((energy.Region, energy.Value));

        }
        public void Change(int index,string name, string atlas, string region, double value, double directionX, 
            double directionY, double directionZ, double positionX, double positionY, double positionZ, IColor color, 
            bool visible)
        {
            Name = name;
            Atlas = atlas;
            _parts[index].Value = value;
            _parts[index].PositionX = positionX*XyzConstrainsModifier.Norm* XyzConstrainsModifier.XConstrin;
            _parts[index].PositionY = positionY * XyzConstrainsModifier.Norm * XyzConstrainsModifier.YConstrin;
            _parts[index].PositionZ = positionZ * XyzConstrainsModifier.Norm * XyzConstrainsModifier.ZConstrin;
            _parts[index].DirectionX= directionX;
            _parts[index].DirectionY= directionY;
            _parts[index].DirectionZ= directionZ;
            _parts[index].Region= region;
            _parts[index].Color= color;
            _parts[index].Visible= visible;
            Vector vector = new Vector(directionX, directionY, directionZ);
            if(index==0)
            {
                ((UniaxialAnisotropy)_energy).Axis.DefaultValue = vector;
                ((UniaxialAnisotropy)_energy).K1.DefaultValue = value;
            }
            else
            {
                ((UniaxialAnisotropy)_energy).Axis.RegionsValue[index-1] = (region, vector);
                ((UniaxialAnisotropy)_energy).K1.RegionsValue[index-1] = (region, value);
            }
        }
        public override void RemoveEnergyPart(IEnergyPart energy)
        {
            _parts.Remove(energy);
            Vector vector = new Vector(energy.DirectionX, energy.DirectionY, energy.DirectionZ);
            ((UniaxialAnisotropy)_energy).Axis.RegionsValue.Remove((energy.Region, vector));
            ((UniaxialAnisotropy)_energy).K1.RegionsValue.Remove((energy.Region, energy.Value));
        }
        public IEnergyPart GetEnergyPart(int index)
        {
            return _parts[index];
        }
    }
}

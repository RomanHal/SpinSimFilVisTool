using CoreLibrary.Interfaces;
using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreLibrary.Positioners
{
    class AnisotropyPositioner : IEnergyPositioner
    {
        private readonly List<IObject> _objects;
        private List<IEnergy> _energies;

        Range X;
        Range Y;
        Range Z;
        double delta;
        public AnisotropyPositioner(List<IObject> objects,List<IEnergy> energies)
        {
            _objects = objects;
            _energies = energies;
        }

        public void ApplyDefaultPosition()
        {
            var anisotropies = _energies.Where(e => e is SpintronicsAnisotropy).Select(e => e as SpintronicsAnisotropy);
            ApplyDefaultInFixed(anisotropies.Where(o => o.Atlas == null));
            ApplyDafaultInUniaxial(anisotropies.Where(e => e.Atlas != null));
        }

        private void ApplyDafaultInUniaxial(IEnumerable<SpintronicsAnisotropy> uniaxials)
        {
            foreach(SpintronicsAnisotropy uniaxial in uniaxials)
            {
                for(int i=1;i< uniaxial.Regions.Length;i++)
                {
                    var atlas = _objects.Find(o => o.Regions.Any(r => r == uniaxial.Regions[i]));
                    if (!(atlas.Xmax >= X.Max && atlas.Xmin <= X.Min))
                        uniaxial.Change(i,uniaxial.Name,uniaxial.Atlas,uniaxial.Regions[i],uniaxial.Values[i],
                            uniaxial.DirectionX[i],uniaxial.DirectionY[i],uniaxial.DirectionZ[i],atlas.Xmax+delta,
                            (atlas.Ymax+atlas.Ymin)/2, (atlas.Zmax + atlas.Zmin)/2,uniaxial.Colors[i],uniaxial.Visibility[i]);
                    else if (!(atlas.Ymax >= Y.Max && atlas.Ymin <= Y.Min))
                        uniaxial.Change(i, uniaxial.Name, uniaxial.Atlas, uniaxial.Regions[i], uniaxial.Values[i],
                             uniaxial.DirectionX[i], uniaxial.DirectionY[i], uniaxial.DirectionZ[i], atlas.Xmax + delta,
                             (atlas.Ymax + atlas.Ymin) / 2, (atlas.Zmax + atlas.Zmin) / 2, uniaxial.Colors[i], 
                             uniaxial.Visibility[i]);
                    else if (!(atlas.Zmax >= Z.Max && atlas.Zmin <= Z.Min))
                        uniaxial.Change(i, uniaxial.Name, uniaxial.Atlas, uniaxial.Regions[i], uniaxial.Values[i],
                            uniaxial.DirectionX[i], uniaxial.DirectionY[i], uniaxial.DirectionZ[i], atlas.Xmax + delta,
                            (atlas.Ymax + atlas.Ymin) / 2, (atlas.Zmax + atlas.Zmin) / 2, uniaxial.Colors[i], 
                            uniaxial.Visibility[i]);

                }
            }
            
        }

        private void ApplyDefaultInFixed(IEnumerable<SpintronicsAnisotropy> fixedAnisotropies)
        {
            SetMaxWorldCoords();
            foreach (var anisotropy in fixedAnisotropies)
            {
               //TODO:Check if placeEmpty
                anisotropy.Change(0, anisotropy.Name, anisotropy.Atlas, anisotropy.Regions[0], anisotropy.Values[0],
                    anisotropy.DirectionX[0],
                    anisotropy.DirectionY[0], anisotropy.DirectionZ[0], X.Min-delta, Y.Min-delta, Z.Min-delta,
                    anisotropy.Colors[0], anisotropy.Visibility[0]);
            }
        }

        private void SetMaxWorldCoords()
        {
            X = new Range(_objects.Min(o => o.Xmin), _objects.Max(o => o.Xmax));
            Y = new Range(_objects.Min(o => o.Ymin), _objects.Max(o => o.Ymax));
            Z = new Range(_objects.Min(o => o.Zmin), _objects.Max(o => o.Zmax));
            delta = (X.Length + Y.Length + Z.Length) / 3;
        }

        private bool FindEmptySpace(double maxX, double deltaX, double maxY, double deltaY)
        {
            return _energies.Where(e => e is SpintronicsAnisotropy).Select(e => e as SpintronicsAnisotropy)
                                .Any(e => (e.PositionX.Any(x => x == maxX + deltaX) && e.PositionY
                                .Any(y => y == maxY + deltaY)));
        }

        public void Refresh()
        {
            ApplyDefaultPosition();
        }
    }
}

using MifParser.Energies;
using MifParser.Fields;
using MifParser.Interfaces;
using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MifParser.Factories
{
    public class AnisotropyFactory
    {
        public IMifEnergy Create(string name,double defaultValue,Vector defaultVector,
            Vector defaultVector2=null,IMifAtlas atlas=null, 
            List<(string,Vector,double)> regionDirectionValueList=null,List<Vector> vector2List=null)
        {
            if(defaultVector2==null)
            {
                IMifScalarField k1;
                IMifVectorField vector;
                if (atlas == null)
                {
                    k1 = new UniformScalarField(defaultValue);
                    vector = new UniformVectorField(defaultVector);
                }
                else
                {
                    k1 = new AtlasScalarField(atlas, defaultValue,
                        regionDirectionValueList.Select(s => (s.Item1, s.Item3)).ToList());
                    vector = new AtlasVectorField(atlas, defaultVector,
                        regionDirectionValueList.Select(s => (s.Item1, s.Item2)).ToList());
                }
                return new UniaxialAnisotropy(name,k1,vector);
            }

            throw new NotImplementedException();
        }
    }
}

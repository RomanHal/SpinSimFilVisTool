using MifParser.Atlases;
using MifParser.Energies;
using MifParser.Fields;
using MifParser.Interfaces;
using MifParser.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MifParser
{
    class MifObjectPacker
    {
        MifObjectsContainer _container = new MifObjectsContainer();

        public MifObjectsContainer Container { get {
                BindScripts();
                BindAtlases();
                return _container;
            } }
        public void Add(string line)
        {
            _container.UnprocessedText.AddLine(line);
        }
        public void Add(string[] lines)
        {
            _container.UnprocessedText.AddLines(lines);
        }
        public void Add(IMifAtlas atlas)
        {
            _container.MifAtlases.Add(atlas);
        }

        public void Add(IMifEnergy energy)
        {
            _container.MifEnergies.Add(energy);
        }

        public void Add(IMifScript script)
        {
            _container.MifScripts.Add(script);
        }

        void BindScripts()
        {
            Action<List<IMifAtlas>> bindScritps = (s => s.Where(a => a is ScriptAtlas).Select(a => (ScriptAtlas)a)
                 .Where(a => a.Script is ScriptNameOnly).ToList().ForEach(a => a.Script = _container.MifScripts
                   .Where(o => a.Script.Name == o.Name).First()));

            _container.MifAtlases.Where(s => s is MultiAtlas).Select(s => (MultiAtlas)s).ToList()
                .ForEach(s => bindScritps(s.MifAtlas));
            bindScritps(_container.MifAtlases);

        }
        void BindAtlases()
        {
            void bindAtlasToUAnisotropy(IMifEnergy e)
            {
                var name = ((AtlasVectorField)((UniaxialAnisotropy)e).Axis).Atlas.Name;
                ((AtlasVectorField)((UniaxialAnisotropy)e).Axis).Atlas = _container.MifAtlases
                .Where(a => a.Name == name).First();
                ((AtlasScalarField)((UniaxialAnisotropy)e).K1).Atlas = _container.MifAtlases
                .Where(a => a.Name == name).First();
            }

            _container.MifEnergies.Where(e => e is UniaxialAnisotropy).Where(e => ((UniaxialAnisotropy)e).Axis is AtlasVectorField)
                .ToList().ForEach(bindAtlasToUAnisotropy);
        }
    }
}

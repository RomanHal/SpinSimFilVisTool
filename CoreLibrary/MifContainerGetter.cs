using CoreLibrary.Interfaces;
using MifParser;
using MifParser.Atlases;
using MifParser.Factories;
using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreLibrary
{
    class MifContainerGetter
    {
        List<IObject> _objects;
        List<IEnergy> _energies;
        List<IScript> _scripts;

        public MifContainerGetter(List<IObject> objects,List<IEnergy> energies, List<IScript> scripts)
        {
            _objects = objects;
            _energies = energies;
            _scripts = scripts;
        }

        internal MifObjectsContainer GetContainer(List<string> multiAtlases,List<List<string>> unprocessedText)
        {
            List<IMifAtlas> atlases = new List<IMifAtlas>();
            List<MultiAtlas> multiAtlas = new List<MultiAtlas>();
            var atlasFactory = new AtlasFactory();
            var container = new MifObjectsContainer();
            multiAtlases.ForEach(a => multiAtlas.Add((MultiAtlas)atlasFactory.Create(a, new List<IMifAtlas>())));
            multiAtlas.ForEach(m => m.MifAtlas.AddRange(
                _objects.Where(o => o is SpintronicsObject).
                Where(o => o.Multiatlas == m.Name).
                Select(s => ((SpintronicsObject)s).MifObject)));
            atlases.AddRange(multiAtlas);
            atlases.AddRange(_objects.Where(o => o is SpintronicsObject).
                Where(o => o.Multiatlas == null || o.Multiatlas == "").
                Select(s => ((SpintronicsObject)s).MifObject));
            container.MifAtlases.AddRange(atlases);
            container.MifEnergies.AddRange(_energies.Where(e => e is SpintronicsEnergy)
                .Select(e => ((SpintronicsEnergy)e).Energy));
            container.MifScripts.AddRange(_scripts.Where(s => s is MifScript)
                .Select(s => ((MifScript)s).GetScriptObject()));
            container.UnprocessedText.Text = unprocessedText;
            return container;
        }
    }
}

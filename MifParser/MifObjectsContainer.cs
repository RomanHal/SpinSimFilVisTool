using MifParser.Interfaces;
using MifParser.Scripts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser
{
    public class MifObjectsContainer
    {
        UnprocessedText _unprocessedText = new UnprocessedText();
        List<IMifAtlas>  _mifAtlases = new List<IMifAtlas>();
        List<IMifEnergy> _mifEnergies = new List<IMifEnergy>();
        List<IMifScript> _mifScripts = new List<IMifScript>();
        public MifObjectsContainer() { }
        public MifObjectsContainer(List<IMifAtlas> atlases,List<IMifEnergy> energies
            ,List<IMifScript> scripts,List<List<string>> uninterpretedText)
        {
            _mifAtlases = atlases;
            _mifEnergies = energies;
            _mifScripts = scripts;
            _unprocessedText.Text = uninterpretedText;
        }
        public List<IMifAtlas> MifAtlases { get => _mifAtlases; }
        public List<IMifEnergy> MifEnergies { get => _mifEnergies; }
        public List<IMifScript> MifScripts { get => _mifScripts;  }
        public UnprocessedText UnprocessedText { get => _unprocessedText; }
    }
}

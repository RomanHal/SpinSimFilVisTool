using MifParser.Atlases;
using MifParser.Enums;
using MifParser.Interfaces;
using MifParser.Parts;
using MifParser.Scripts;
using System;
using System.Collections.Generic;

namespace MifParser.Factories
{
    public class AtlasFactory
    {
        internal IMifAtlas Create(string name, Range x,Range y,Range z,AtlasType type,List<string> regions,string scriptName=null,string scriptArgs=null)
        {
            switch(type)
            {
                case AtlasType.BoxAtlas: return new BoxAtlas(name, x, y, z, regions[0]);
                case AtlasType.ScriptAtlas: return new ScriptAtlas(name, x, y, z, regions, scriptArgs, new ScriptNameOnly(scriptName));
                case AtlasType.ImageAtlas: throw new NotImplementedException();
            }
            throw new NotImplementedException();
        }
        public IMifAtlas Create(string name, Range x, Range y, Range z, AtlasType type, List<string> regions, IMifScript script = null, string scriptArgs = null)
        {
            switch (type)
            {
                case AtlasType.BoxAtlas: return new BoxAtlas(name, x, y, z, regions[0]);
                case AtlasType.ScriptAtlas: return new ScriptAtlas(name, x, y, z, regions, scriptArgs, script);
                case AtlasType.ImageAtlas: throw new NotImplementedException();
            }
            throw new NotImplementedException();
        }

        public IMifAtlas Create(string name, List<IMifAtlas> atlases)
        {
            return new MultiAtlas(name, atlases);
        }

    }
}

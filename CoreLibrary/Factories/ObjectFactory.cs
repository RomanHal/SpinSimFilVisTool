using CoreLibrary.Enums;
using CoreLibrary.GraphicsServices;
using CoreLibrary.Interfaces;
using MifParser.Factories;
using MifParser.Interfaces;
using MifParser.Parts;
using MifParser.Scripts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.Factories
{
    public class ObjectFactory
    {
        GraphicCreator _graphicCreator = new GraphicCreator();
        AtlasFactory _atlasFactory = new AtlasFactory();
        public IObject Create(string name, AtlasType type,double xMin,double xMax, double yMin, double yMax, 
            double zMin, double zMax, IColor color,List<string>regions,IScript script,string ScriptArgs)
        {
            IMifScript mifScript =script!=null? new ScriptObject(script.Name, script.Text, script.Args):null;
            var mifObject = _atlasFactory.Create(name, new Range(xMin, xMax), new Range(yMin, yMax),
                new Range(zMin, zMax), ConvertAtlasType(type), regions, mifScript, ScriptArgs);
            var graphicObject = _graphicCreator.CreateGraphic(mifObject,color);
            return new SpintronicsObject(mifObject, graphicObject);
        }

        MifParser.Enums.AtlasType ConvertAtlasType(AtlasType type)
        {
            switch(type)
            {
                case AtlasType.BoxAtlas:
                    return MifParser.Enums.AtlasType.BoxAtlas;
                case AtlasType.ScriptAtlas:
                    return MifParser.Enums.AtlasType.ScriptAtlas;
                case AtlasType.MultiAtlas:
                    return MifParser.Enums.AtlasType.MultiAtlas;
                default: throw new NotSupportedException();
            }

        }
    }
}

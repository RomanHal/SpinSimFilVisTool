using MifParser.Enums;
using MifParser.Utility;
using MifParser.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using MifParser.Scripts;
using System.Linq;

namespace MifParser
{
    public class FileReader
    {
        List<string> _file;
        MifObjectPacker _packer=new MifObjectPacker();
        TextProcessingHelper _helper = new TextProcessingHelper();
        AtlasFactoryText _atlasFactory = new AtlasFactoryText();
        ScriptFactoryText _scriptFactory = new ScriptFactoryText();
        EnergyFactoryText _energyFactory = new EnergyFactoryText();

        public FileReader()
        {
            _packer.Container.UnprocessedText.AddLines(new List<string>());
            _energyFactory.UnprocessedText = _packer.Container.UnprocessedText.Text;
        }
        public MifObjectsContainer ReadFile(string patch)
        {
            _file = new List<string>(new FileLoader().LoadFile(patch));
            return GetMifObjects();
        }

        void Read()
        {
            for(int i=0;i<_file.Count;i++)
            {
               switch(_file[i])
                {
                    case string s when s == "":
                        break;
                    case string s when s[0] == '#':
                        _packer.Container.UnprocessedText.AddLine(_file[i]);
                        break;
                    case string s when s.Contains("proc"):
                        var script = _helper.GetCodeToClosingBrace(_file.ToArray(), i,  '{');
                        i=_helper.SearchClosingBraces(_file.ToArray(), '{', i);
                        _packer.Add(_scriptFactory.CreateScript(script));
                        break;
                    case string s when s.Contains("Specify") && s.Contains("Atlas"):
                        var atlas = _helper.GetCodeToClosingBrace(_file.ToArray(), i, '{', '[');
                        i = _helper.SearchClosingBraces(_file.ToArray(), i, '{', '[');
                        _packer.Add(_atlasFactory.CreateAtlas(atlas));
                        break;
                    case string s when s.Contains("Specify") && s.Contains("Mesh"):
                        var mesh=_helper.GetCodeToClosingBrace(_file.ToArray(), i, '{');
                        i= _helper.SearchClosingBraces(_file.ToArray(), i, '{');
                        _packer.Container.UnprocessedText.AddLines(mesh); 
                        break;
                    case string s when s.Contains("Specify") && s.Contains("Evolve"):
                        var evolve = _helper.GetCodeToClosingBrace(_file.ToArray(), i, '{');
                        i=_helper.SearchClosingBraces(_file.ToArray(), i, '{');
                        _packer.Container.UnprocessedText.AddLines(evolve);
                        break;
                    case string s when s.Contains("Specify"):
                        var energyText= _helper.GetCodeToClosingBrace(_file.ToArray(), i, '{');
                        i = _helper.SearchClosingBraces(_file.ToArray(), i, '{');
                        var energy = _energyFactory.CreateEnergy(energyText);
                        if(energy!=null) _packer.Add(energy);
                        break;
                    default:
                        _packer.Container.UnprocessedText.AddLine(_file[i]);
                        break;
                }
            }
        }

        MifObjectsContainer GetMifObjects()
        {
            Read();
            return _packer.Container;
        }
    }
}

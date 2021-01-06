using MifParser.Energies;
using MifParser.Interfaces;
using MifParser.Parts;
using MifParser.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MifParser.Factories
{
    class ZeemanFactoryText
    {

        const string uZeeman = "Oxs_UZeeman";
        const string hRange = "Hrange";
        private const string _multiplier = "multiplier";
        TextProcessingHelper _helper = new TextProcessingHelper();
        ScalarFieldFactoryText scalarFieldFactory = new ScalarFieldFactoryText();
        VectorFieldFactoryText vectorFieldFactory = new VectorFieldFactoryText();
        public List<List<string>> UnprocessedText { set; private get; }
        
        public IMifEnergy Create(string[] text)
        {
            switch(text[0])
            {
                case string s when s.Contains(uZeeman):
                    return CreateUZeeman(text);

                default:
                    UnprocessedText[0].AddRange(text);
                    return null;
            }

        }

        private IMifEnergy CreateUZeeman(string[] text)
        {
            string name = _helper.GetName(text[0]);
            var index = _helper.GetIndexOfStringContaining(text, hRange);
            var list = GetHRange(_helper.GetCodeToClosingBrace(text, index, '{'));
            var multiplier = text.Where(s => s.Contains(_multiplier)).
                Select(s => s.Substring(s.IndexOf(_multiplier) + _multiplier.Length)).FirstOrDefault();
            multiplier = multiplier.Replace(" ", "");
            double multiplierValue = double.Parse(multiplier);
            return new UZeeman(name, multiplierValue, list);
        }

        List<(Vector,Vector,int)> GetHRange(string[] lines)
        {
            List<(Vector, Vector, int)> list = new List<(Vector, Vector, int)>();
            var text = _helper.RemoveFromText(lines, "{", "}", "Hrange");
            foreach (string line in text)
            {
                if(line!="")
                if(line!=" ")
                if (line[0] != '#')
                {
                    var data = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    Vector start = new Vector(double.Parse(data[0]), double.Parse(data[1]), double.Parse(data[2]));
                    Vector end = new Vector(double.Parse(data[3]), double.Parse(data[4]), double.Parse(data[5]));
                    list.Add((start, end, int.Parse(data[6])));
                }
            }
            return list;
        }
        
    }
}

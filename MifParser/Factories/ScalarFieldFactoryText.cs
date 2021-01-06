using MifParser.Atlases;
using MifParser.Fields;
using MifParser.Interfaces;
using MifParser.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MifParser.Factories
{
    class ScalarFieldFactoryText
    {
        const string atlasField = "Oxs_AtlasScalarField";
        private TextProcessingHelper helper = new TextProcessingHelper();
        public IMifScalarField Create(string[] lines)
        {
           switch(lines[0])
            {
                case string s when s.Contains(atlasField):return CreateAtlasField(lines);


                default: throw new NotSupportedException();

            }

        }

        private IMifScalarField CreateAtlasField(string[] lines)
        {
            string atlas = lines.Where(s => s.Contains("atlas")).FirstOrDefault();
            var atlasIEnumerable    = atlas.Select((c, i) => new { c, i }).
                Where((c, i) => i > atlas.IndexOf(':')).Select(s => s.c);
            atlas = string.Concat(atlasIEnumerable).Replace(" ", "");
            string valueString = lines.Where(s => s.Contains("default_value")).FirstOrDefault().Replace("default_value", "");
            valueString = valueString.Replace(" ", "");
            double value = double.Parse(valueString);
            int lineNumber = helper.GetIndexOfStringContaining(lines, "values");
            var text = helper.GetCodeToClosingBrace(lines, lineNumber, '{');
            var list = new List<(string, double)>();
            for (int i = 1; i < text.Length - 1; i++)
            {
                var values = text[i].Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
                list.Add((values[0], double.Parse(values[1])));
            }
            
            
            return new AtlasScalarField(new AtlasNameOnly(atlas),value,list);
        }
    }
}

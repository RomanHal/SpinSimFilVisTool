using MifParser.Atlases;
using MifParser.Fields;
using MifParser.Interfaces;
using MifParser.Parts;
using MifParser.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MifParser.Factories
{
    class VectorFieldFactoryText
    {
        const string atlasField = "Oxs_AtlasVectorField";
        const string uniformField = "Oxs_UniformVectorField";
        TextProcessingHelper helper = new TextProcessingHelper();
        public IMifVectorField Create(string[] lines)
        {
            switch(lines[0])
            {
                case string s when s.Contains(atlasField):
                    return CreateAtlasField(lines);
                case string s when s.Contains(uniformField):
                    return CreateUniformField(lines);

                default:throw new NotSupportedException();
            }
        }

        private IMifVectorField CreateUniformField(string[] lines)
        {
            string normText = lines.Where(s => s.Contains("norm")).FirstOrDefault();
            double norm = 0;
            if(normText!=null)
            {
                normText = normText.Replace("norm", "");
                normText = normText.Replace(" ", "");
                norm = double.Parse(normText);
            }
            var vectorText = lines.Where(s => s.Contains("vector")).FirstOrDefault()
                .Replace("vector", "");
            var vector = CreateVector(vectorText);
            return new UniformVectorField(vector,norm);
        }

        private IMifVectorField CreateAtlasField(string[] lines)
        {
            string atlas = lines.Where(s => s.Contains("atlas")).FirstOrDefault();
            var atlasIEnumerable = atlas.Select((c, i) => new { c, i }).
                Where((c, i) => i > atlas.IndexOf(':')).Select(s => s.c);
            atlas = string.Concat(atlasIEnumerable).Replace(" ", "");
            string valueVectorString = lines.Where(s => s.Contains("default_value"))
                .FirstOrDefault().Replace("default_value", "");
            Vector vector = CreateVector(valueVectorString);
            int lineNumber = helper.GetIndexOfStringContaining(lines, "values");
            var text = helper.GetCodeToClosingBrace(lines, lineNumber, '{');
            text = helper.RemoveFromText(text, "{", "}");
            var list = new List<(string, Vector)>();
            for (int i = 1; i < text.Length - 1; i++)
            {
                var values = text[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                list.Add((values[0],new Vector(double.Parse(values[1]), double.Parse(values[2]),
                    double.Parse(values[3]))));
            }


            return new AtlasVectorField(new AtlasNameOnly(atlas), vector, list);
        }


        public Vector CreateVector(string line)
        {
            StringBuilder builder = new StringBuilder(line);
            builder = builder.Replace("{", "");
            builder = builder.Replace("}", "");
            builder = builder.Replace('\t', ' ');
            var data = builder.ToString().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            return new Vector(double.Parse(data[0]), double.Parse(data[1]), double.Parse(data[2]));
        }
    }
}

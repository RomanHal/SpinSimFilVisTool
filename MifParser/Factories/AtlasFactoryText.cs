using MifParser.Atlases;
using MifParser.Interfaces;
using MifParser.Parts;
using MifParser.Scripts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MifParser.Factories
{
    internal class AtlasFactoryText
    {
        CultureInfo culture = new CultureInfo("en-US");
        const string atlas = "atlas";
        const string multiAtlas = "Oxs_MultiAtlas";
        const string scriptAtlas = "Oxs_ScriptAtlas";
        const string boxAtlas = "Oxs_BoxAtlas";
        const string imageAtlas = "Oxs_ImageAtlas";
        const string range = "range";
        Utility.TextProcessingHelper textHelper = new Utility.TextProcessingHelper();


        public IMifAtlas CreateAtlas(string[] lines)
        {
            switch(lines[0])
            {
                case string s when (s.Contains(multiAtlas)): return CreateMultiatlas(lines);
                case string s when (s.Contains(scriptAtlas)):return CreateScriptAtlas(lines);
                case string s when (s.Contains(boxAtlas)): return CreateBoxAtlas(lines);
                case string s when (s.Contains(imageAtlas)): return CreateImageAtlas(lines);
                default: throw new  NotSupportedException("Unsupported Atlas Type: "+ lines[0]);
            }
        }

        private Atlas CreateImageAtlas(string[] lines)
        {
            throw new NotImplementedException("Image Atlas is not supported");
        }

        private Atlas CreateBoxAtlas(string[] lines)
        {
            var name = GetAtlasName(lines[0]);
            var ranges = CreateAtlasRangesXYZ(lines);
            var region = lines.Where(s => s.Contains("region")).Select(s =>s = s.Replace("region", "")).FirstOrDefault();
            return new BoxAtlas(name, ranges[0], ranges[1], ranges[2], region);
        }

        MultiAtlas CreateMultiatlas(string[] lines)
        {
            string name = GetAtlasName(lines[0]);
            var list = new List<IMifAtlas>();
            lines.Select((s, i) => new { s, i})
                .Where(o => { return o.s.Contains(atlas) && !o.s.Contains(multiAtlas); })
                .Select(s => s.i).ToList().ForEach(i=>{
                    var AtlasLines = textHelper.GetCodeToClosingBrace(lines, i,  '{');
                    list.Add(CreateAtlas(AtlasLines));
                });
                      
            return new MultiAtlas(name,list);
        }

        ScriptAtlas CreateScriptAtlas(string[] lines)
        {
            string name = GetAtlasName(lines[0]);
            Range[] xyzRanges = CreateAtlasRangesXYZ(lines);
            var regions = lines.Where(s => s.Contains("regions")).Select(s => 
            {
                s = s.Replace("regions", "");
                s = s.Replace("{", "");
                s = s.Replace("}", "");
                return s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            }).ToList();
            string scriptName = lines.Where(s => s.Contains("script")&&!s.Contains("script_args")).Select(s =>
            {
                s = s.Replace("script", "");
                return s.Replace(" ","");
            }).FirstOrDefault();
            string scriptArgs = lines.Where(s => s.Contains("script_args")).Select(s =>
                {
                    s = s.Replace("script_args", "");
                    s = s.Replace("{", "");
                    s = s.Replace("}", "");
                    return s.Replace(" ", "");
                }).FirstOrDefault();

            return new ScriptAtlas(name,xyzRanges[0], xyzRanges[1], xyzRanges[2], regions,scriptArgs , new ScriptNameOnly(scriptName));///TODO script
        }
        Range[] CreateAtlasRangesXYZ(string[] lines)
        {
            string xRange = lines.Where(s => s.Contains("x" + range)).First();
            Range x = GetRangeFromLine("x" + range, xRange);
            string yRange = lines.Where(s => s.Contains("y" + range)).First();
            Range y = GetRangeFromLine("y" + range, yRange);
            string zRange = lines.Where(s => s.Contains("z" + range)).First();
            Range z = GetRangeFromLine("z" + range, zRange);
            return new Range[] { x, y, z };
        }
        Range GetRangeFromLine(string rangeName,string line)
        {
            line= line.Replace(rangeName, "");
            line = line.Replace("{", "");
            line = line.Replace("}", "");
            var values= line.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            double minValue = double.Parse(values[0],culture);
            double maxValue = double.Parse(values[1],culture);
            return new Range(minValue, maxValue);
        }
        string GetAtlasName(string line)
        {
            var name = line.Select((c, i) => new { c, i }).
                  Where(o => o.i > line.LastIndexOf(':')).
                  Where(o => line.IndexOf('[') != -1 ? o.i < line.IndexOf('[') : o.i > -1).
                  Where(o => o.i < line.LastIndexOf('{')).
                  Select(o => o.c);
            string nameStr = string.Concat(name);
            return nameStr.Replace(" ","");
        }

    }
}

using MifParser.Atlases;
using MifParser.Interfaces;
using MifParser.Parts;
using MifParser.Scripts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace Parser.Tests
{
    public class MultiAtlasTests
    {
        [Fact]
        public void GetTextTest()
        {
            string[] lines = new string[] { "Oxs_MultiAtlas:test1 [subst {" ,"","atlas { Oxs_ScriptAtlas:name1 {", "xrange { 0 0.2 }","yrange { 0.1 0.3 }","zrange { 0 0.2 }",
                "regions { asdf }","script_args { XYZ }","script scName","} }","","atlas { Oxs_ScriptAtlas:name2 {","xrange { 0 1.2 }",
                "yrange { 0.1 1.3 }","zrange { 0 1.2 }","regions { AAasdf }","script_args { AXYZ }","script scName2","} }","","","}]" };
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            List<IMifAtlas> atlases = new List<IMifAtlas>();
            atlases.Add(new ScriptAtlas("name1", new Range(0, 0.2), new Range(0.1, 0.3), new Range(0, 0.2), new List<string> { "asdf" }, "XYZ", new ScriptObject("scName", "213", "AAA")));
            atlases.Add(new ScriptAtlas("name2", new Range(0, 1.2), new Range(0.1, 1.3), new Range(0, 1.2), new List<string> { "AAasdf" }, "AXYZ", new ScriptObject("scName2", "214", "BBB")));
            var atlas = new MultiAtlas("test1", atlases);
            string[] AtlasLines = atlas.GetText();


            Assert.Equal(lines, AtlasLines);

        }
    }
}

using MifParser.Atlases;
using MifParser.Parts;
using MifParser.Scripts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace Parser.Tests
{
    public class ScriptAtlasTests
    {
        [Fact]
        public void GetTextTest()
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            ScriptAtlas atlas = new ScriptAtlas("scriptatlas", new Range(0.2, 0.3), new Range(0.1, 0.2)
                , new Range(0.3, 0.4), new List<string>() { "region", "second" }, 
                "scriptArg", new ScriptObject("sscript", "", ""));

            var expected = new string[]
            {
                "Oxs_ScriptAtlas:scriptatlas {",
                "xrange { 0.2 0.3 }",
                "yrange { 0.1 0.2 }",
                "zrange { 0.3 0.4 }",
                "regions { region second }",
                "script_args { scriptArg }",
                "script sscript","}"
            };
            var test = atlas.GetText();

            Assert.Equal(expected,test);
        }
    }
}

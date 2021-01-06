using MifParser.Atlases;
using MifParser.Factories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace Parser.Tests
{
    public class AtlasFactoryTextTests
    {
        [Fact]
        public void CreateMultiatlasTest()
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            var creator = new AtlasFactoryText();
            var text = new string[] { "Specify Oxs_MultiAtlas:multiatlas1[subst {" ,"", "atlas { Oxs_ScriptAtlas: top {",
" xrange { 0e-9 70e-9}","yrange { 0e-9 70e-9}","zrange { 2.1e-9 3.5e-9}","regions { top emptytop}","script_args { relpt }",
"script EllipseZ","} }","","atlas { Oxs_ScriptAtlas: spacer {","xrange { 0e-9 70e-9}","yrange { 0e-9 70e-9}","zrange { 0.7e-9 2.1e-9}",
"regions { spacer emptyspacer}","script_args { relpt }","script EllipseZ","} }","","atlas { Oxs_ScriptAtlas: bottom {",
"xrange { 0e-9 70e-9}","yrange { 0e-9 70e-9}","zrange { 0e-9 0.7e-9}","regions { bottom emptybottom}","script_args { relpt }",
"script EllipseZ","}","","}]" };
            var atlas = creator.CreateAtlas(text);
            Assert.IsType<MultiAtlas>(atlas);
        }

        [Fact]
        public void CreateScriptAtlas()
        {
            var creator = new AtlasFactoryText();
            ///TODO
        }
    }
}

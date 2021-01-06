using MifParser.Atlases;
using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;



namespace Parser.Tests
{
    public class BoxAtlasTests
    {
        
        [Fact]
        public void GetTextTest()
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            BoxAtlas atlas = new BoxAtlas("boxatlas", new Range(0.1, 0.5), new Range(0.2, 0.6),
                new Range(0.3, 0.7), "ReGiOn");
            var expected = new string[] {
            "Oxs_BoxAtlas:boxatlas {",
            "xrange {0.1 0.5}",
            "yrange {0.2 0.6}",
            "zrange {0.3 0.7}",
            "region ReGiOn",
            "}"};
            Assert.Equal(expected, atlas.GetText());
        }
    }
}

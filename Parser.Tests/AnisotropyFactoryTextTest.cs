using MifParser.Energies;
using MifParser.Factories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace Parser.Tests
{
    public class AnisotropyFactoryTextTest
    {
        AnisotropyFactoryText factory = new AnisotropyFactoryText();
        [Fact]
        public void CreateUniaxalAnisotopyTest()
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            var text = new string[] { "Specify Oxs_UniaxialAnisotropy:IP {", "K1 { Oxs_AtlasScalarField {",
                "atlas: multiatlas1", "default_value 0", "values {", "top 7.5e3", "}", "} }",
                "axis { Oxs_AtlasVectorField {", "atlas: multiatlas1", "default_value { 1 0 0 }",
                "values {", "top { 1 0 0 }", "}", "} }", "}"};
            UniaxialAnisotropy anisotropy = factory.CreateAnisotropy(text) as UniaxialAnisotropy;

            Assert.Equal("IP",anisotropy.Name);

        }
    }
}

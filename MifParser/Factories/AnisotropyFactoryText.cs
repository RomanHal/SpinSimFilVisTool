using MifParser.Energies;
using MifParser.Interfaces;
using MifParser.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Factories
{
    class AnisotropyFactoryText
    {
        const string uniaxal = "Oxs_UniaxialAnisotropy";
        const string cubic = "Oxs_CubicAnisotropy";
        TextProcessingHelper helper = new TextProcessingHelper();
        ScalarFieldFactoryText scalarFieldFactory = new ScalarFieldFactoryText();
        VectorFieldFactoryText vectorFieldFactory = new VectorFieldFactoryText();
        public List<List<string>> UnprocessedText { set; private get; }
        public IMifEnergy CreateAnisotropy(string[] lines)
        {
            switch(lines[0])
            {
                case string s when s.Contains(uniaxal):
                    return CreateUniaxialAnisotropy(lines);
                case string s when s.Contains(cubic):
                    return CreateCubicAnisotropy(lines);
                default: UnprocessedText[0].AddRange(lines);
                    return null;
            }
        }

        private IMifEnergy CreateCubicAnisotropy(string[] lines)
        {
            string name = helper.GetName(lines[0]);
            int number = helper.GetIndexOfStringContaining(lines, "K1");
            var textK1 = helper.GetCodeToClosingBrace(lines, number, '{');
            IMifScalarField k1 = scalarFieldFactory.Create(textK1);
            number = helper.GetIndexOfStringContaining(lines, "axis1");
            var textAxis = helper.GetCodeToClosingBrace(lines, number, '{');
            IMifVectorField axis1 = vectorFieldFactory.Create(textAxis);
            number = helper.GetIndexOfStringContaining(lines, "axis2");
            textAxis = helper.GetCodeToClosingBrace(lines, number, '{');
            IMifVectorField axis2 = vectorFieldFactory.Create(textAxis);
            return new CubicAnisotropy(name, k1, axis1, axis2);
        }

        private IMifEnergy CreateUniaxialAnisotropy(string[] lines)
        {
            string name = helper.GetName(lines[0]);
            int number = helper.GetIndexOfStringContaining(lines, "K1");
            var textK1 = helper.GetCodeToClosingBrace(lines, number, '{');
            IMifScalarField k1 = scalarFieldFactory.Create(textK1);
            number = helper.GetIndexOfStringContaining(lines, "axis");
            var textAxis = helper.GetCodeToClosingBrace(lines, number, '{');
            IMifVectorField axis = vectorFieldFactory.Create(textAxis);
            return new UniaxialAnisotropy(name, k1, axis);
        }
    }
}

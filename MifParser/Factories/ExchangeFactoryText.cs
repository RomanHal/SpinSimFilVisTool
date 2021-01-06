using MifParser.Atlases;
using MifParser.Energies;
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
    class ExchangeFactoryText
    {
        private TextProcessingHelper _helper = new TextProcessingHelper();
        const string _twoSurfaceExchange = "Oxs_TwoSurfaceExchange";
        private const string _atlas = "atlas";
        private const string _region = "region";
        private const string _scalarField = "scalarfield";
        private const string _scalarValue = "scalarvalue";
        private const string _scalarSide = "scalarside";
        private const string _surface1 = "surface1";
        private const string _surface2 = "surface2";
        private const string _sigma = "sigma";

        public List<List<string>> UnprocessedText { private get; set; }
        public IMifEnergy Create(string[] lines)
        {
            switch(lines[0])
            {
                case string s when s.Contains(_twoSurfaceExchange):
                    return CreateTwoSurfaceExchange(lines);
                default:UnprocessedText[0].AddRange(lines);
                    return null;
            }
        }

        private IMifEnergy CreateTwoSurfaceExchange(string[] lines)
        {
            Surface surface1, surface2;
            double sigma1, sigma2;
            string name = _helper.GetName(lines[0]);
            var index= _helper.GetIndexOfStringContaining(lines, _surface1);
            var surfaceText = _helper.GetCodeToClosingBrace(lines, index, '{');
            surface1 = CreateSurface(surfaceText);
            index = _helper.GetIndexOfStringContaining(lines, _surface2);
            surfaceText = _helper.GetCodeToClosingBrace(lines, index, '{');
            surface2 = CreateSurface(surfaceText);
            var sigmaText = GetValueOf(_sigma, lines);
            sigma1 = double.Parse(sigmaText);
            sigmaText = GetValueOf(_sigma+"2",lines);
            sigma2 = double.Parse(sigmaText);

            return new TwoSurfaceExchange(name,sigma1, sigma2, surface1, surface2);
        }

        private Surface CreateSurface(string[] lines)
        {
            var atlasText = GetValueOf(_atlas, lines);
            var atlas = new AtlasNameOnly(atlasText);
            var regionText = GetValueOf(_region, lines);
            var scalarField = new ScalarFieldName( GetValueOf(_scalarField,lines));
            var scalarFieldValue = double.Parse(GetValueOf(_scalarValue, lines));
            var scalarSide = GetValueOf(_scalarSide, lines);
            bool sign = scalarSide == "+" ? true : false;
            return new Surface(atlas,regionText,scalarField,scalarFieldValue,sign);
        }
        private string GetValueOf(string fieldName,string[] lines)
        {
            var value= lines.Where(s => s.Contains(fieldName)).FirstOrDefault();
            value = value.Replace(fieldName, "");
            value = value.Replace(" ", "");
            return value;
        }
    }
}

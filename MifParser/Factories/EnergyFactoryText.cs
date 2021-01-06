using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Factories
{
    class EnergyFactoryText
    {
        const string anisotropy = "Anisotropy";
        const string zeeman = "Zeeman";
        AnisotropyFactoryText _anisotropyFactory = new AnisotropyFactoryText();
        ZeemanFactoryText _zeemanFactory = new ZeemanFactoryText();
        public List<List<string>> UnprocessedText { set =>SetUnprocessedText(value); }
        List<List<string>> _unprocessedText;

        public EnergyFactoryText()
        {
        }
        public IMifEnergy CreateEnergy(string[] lines)
        {
          switch(lines[0])
            {
                case string s when s.Contains(anisotropy):
                    return _anisotropyFactory.CreateAnisotropy(lines);
                case string s when s.Contains(zeeman):
                    return _zeemanFactory.Create(lines);





                default:_unprocessedText[0].AddRange(lines);
                    return null;
            }
        }
        private void SetUnprocessedText(List<List<string>> unprocessedText)
        {
            _unprocessedText = unprocessedText;
            _anisotropyFactory.UnprocessedText = unprocessedText;
            _zeemanFactory.UnprocessedText = unprocessedText;
        }
    }
}

using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Energies
{
    class RandomSiteExchange : Energy
    {
        double Probability { get; set; }
        double Amin { get; set; }
        double Amax { get; set; }

        public RandomSiteExchange(string name) : base(name)
        {
            throw new NotImplementedException();
        }

        internal override string[] GetText()
        {
            throw new NotImplementedException();
        }
    }
}

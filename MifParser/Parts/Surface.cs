using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Parts
{
    public class Surface
    {
        IMifAtlas Atlas { get; set; }
        string Region { get; set; }
        IMifScalarField ScalarField { get; set; }
        double ScalarFieldValue { get; set; }
        bool ScalarSideSign { get; set; }
        public Surface() { }
        public Surface(IMifAtlas atlas,string region,IMifScalarField scalarField,double scalarFieldValue,bool scalarSideSign)
        {
            Atlas = atlas;
            Region = region;
            ScalarField = scalarField;
            ScalarFieldValue = scalarFieldValue;
            ScalarSideSign = scalarSideSign;
        }
        internal string[] GetText()
        {//TODO
            string sign = ScalarSideSign ? "+" : "-";
            return new string[] {"atlas :"+Atlas.Name,"region "+Region, "scalarfield :"+ScalarField.Name,"scalarvalue :"
                +ScalarFieldValue,"scalarside "+sign
            };
        }
    }
}

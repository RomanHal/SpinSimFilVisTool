using System;
using System.Collections.Generic;
using System.Text;

namespace MifParser.Atlases
{
    class ImageAtlas:Atlas
    {
        string Image { get; set; }
        string ViewPlane { get; set; }
        public override List<string> Regions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        Dictionary<string, string> colorRegionMap = new Dictionary<string, string>();

        public ImageAtlas(string name,double xMax, double xMin, double yMax, double yMin, double zMax, double zMin, 
            string image, string viewPlane, Dictionary<string, string> colorRegionMap)
            : base(name,xMax, xMin, yMax, yMin, zMax, zMin)
        {
            Image = image;
            ViewPlane = viewPlane;
            this.colorRegionMap = colorRegionMap;
        }

        internal override string[] GetText()
        {
            throw new NotImplementedException();
        }

        ///TODO how preset it in program


    }
}

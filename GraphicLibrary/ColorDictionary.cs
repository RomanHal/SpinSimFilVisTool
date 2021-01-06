using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;



namespace GraphicLibrary
{
    public class ColorDictionary
    {
        IDictionary<FigureColor, OpenTK.Color> _colorDictionary = new Dictionary<FigureColor, OpenTK.Color>();

        public   ColorDictionary()
        {
            foreach(FigureColor color in Enum.GetValues(typeof(FigureColor)))
            {
                if(color!=FigureColor.Custom)
                {

                    OpenTK.Color ColorTk = new OpenTK.Color();

                    PropertyInfo OpenTkColorProperty = typeof(OpenTK.Color).GetProperty(color.ToString(), typeof(OpenTK.Color));
                    ColorTk = (OpenTK.Color)OpenTkColorProperty.GetValue(null);

                    _colorDictionary.Add(color, ColorTk);
                }
            }
        }

        public OpenTK.Color GetColor(FigureColor color,int[] ARGB=null)
        {
            
            if(color!=FigureColor.Custom)
                return _colorDictionary[color];
            return OpenTK.Color.FromArgb(255,ARGB[0], ARGB[1], ARGB[2]);
        }
    }
}

using CoreLibrary.Interfaces;
using GraphicLibrary.Interfaces;
using MifParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.GraphicsServices.Interfaces
{
    interface IGraphicCreator
    {
        IGraphicsObject CreateGraphic(IMifAtlas atlas,IColor color);
    }
}

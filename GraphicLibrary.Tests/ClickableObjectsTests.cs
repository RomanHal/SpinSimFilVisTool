using GraphicLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class ClickableObjectsTests
    {
        [Fact]
        public void PointBelongToTest()
        {
            GraphicLibrary.Interfaces.IGraphicsObject myShape = new GraphicLibrary.FigureFactory().Create(GraphicLibrary.FigureType.Cuboid,
                GraphicLibrary.Point.GetPoint(0, 0, 2), GraphicLibrary.Point.GetPoint(0, 0, 0), 4.0, 4.0, GraphicLibrary.FigureColor.Red);
            myShape.Visible = true;
            

            Assert.True(((GraphicLibrary.ClickableObject)myShape).PointBelongsTo(GraphicLibrary.Point.GetPoint(0, 2, 2)));
        }
    }
}

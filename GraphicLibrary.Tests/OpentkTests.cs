using Xunit;
using GraphicLibrary;
using GraphicLibrary.Management;

namespace UnitTests
{
    public class OpentkTests
    {
       // [Fact]
        public void GraphicalTest()
        {
            double x=0, y=0;
            FigureContainer.AddElement("Asd", FigureColor.Blue, FigureType.Cuboid, Point.GetPoint(0, 1, 2), Point.GetPoint(1, 2, 2),new double[] { 3,3},null);

            new MyWindow(600, 800, ref x, ref y).Run();
           
            
        }
    }
}

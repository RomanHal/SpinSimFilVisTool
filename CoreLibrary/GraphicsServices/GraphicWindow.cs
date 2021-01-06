using System.Threading.Tasks;

namespace CoreLibrary.GraphicsServices
{
    public class GraphicWindow
    {
        public bool IsOpen { get; set; }
        public void OpenWindow(int width, int height)
        {
            Task task = new Task(() =>
            {
                using (var myWindow = new GraphicLibrary.MyWindow(width, height,"Graphic Window"))
                    myWindow.Run(10.0);
            });
            task.Start();
            IsOpen = true;
        }
        //closing windowCommand
    }
}

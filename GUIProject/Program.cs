using System;
using System.Globalization;
using System.IO;
using Avalonia;
using Avalonia.Logging.Serilog;
using GUIProject.Views;
using Serilog;

namespace GUIProject
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            LoggerInit();
            if (!Directory.Exists("PrintScreens")) Directory.CreateDirectory("PrintScreens");
            if (!Directory.Exists("Logs")) Directory.CreateDirectory("Logs");

            //try
            {
                BuildAvaloniaApp().Start<MainWindow>();
            }
            //catch(Exception e)
            //{
            //    Log.Error(e.ToString());
            //    throw e;
            //}
        }

        private static void LoggerInit()
        {
            SerilogLogger.Initialize(new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.File("Logs/GUILog.log")
                .CreateLogger());
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.File("Logs/Errors.log")
                .CreateLogger();
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                ;

    }
}

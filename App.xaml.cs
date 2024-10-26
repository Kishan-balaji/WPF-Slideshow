using System.Configuration;
using System.Data;
using System.Windows;

namespace WPF_Slideshow
{
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            WindowManager.OpenWindows();
        }
    }

    public class WindowManager
    {
        private static bool windowsOpened = false;

        public static void OpenWindows()
        {
            if (!windowsOpened)
            {
                windowsOpened = true;
                Screen[] screens = Screen.AllScreens;
                for (int i = 1; i < screens.Length; i++)
                {
                    Window window = new MainWindow();
                    window.WindowState = WindowState.Normal;
                    window.WindowStartupLocation = WindowStartupLocation.Manual;
                    window.Left = screens[i].WorkingArea.Left;
                    window.Top = screens[i].WorkingArea.Top;
                    window.Width = screens[i].WorkingArea.Width;
                    window.Height = screens[i].WorkingArea.Height;
                    window.Show();
                    window.WindowStyle = WindowStyle.None;
                    window.WindowState = WindowState.Maximized;
                }
            }
        }
    }
}

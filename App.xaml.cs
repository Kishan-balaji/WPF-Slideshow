using System.Configuration;
using System.Data;
using System.Windows;

namespace WPF_Slideshow;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    
        Screen[] screens = Screen.AllScreens;       
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            int screenCount = Screen.AllScreens.Length;
            for (int i = 1; i < screenCount; i++)
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



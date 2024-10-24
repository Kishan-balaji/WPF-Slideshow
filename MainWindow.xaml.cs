using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;


namespace WPF_Slideshow
{
    public partial class MainWindow : Window
{
    private readonly string[] mediaFiles;
    private readonly TimeSpan displayDuration = TimeSpan.FromSeconds(5);
    private int currentIndex = 0;
    private readonly DispatcherTimer imageTimer;
    
    public MainWindow()
    {
        InitializeComponent();
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string assetsPath = System.IO.Path.Combine(baseDirectory, "../../../Assets");
        var supportedImages = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
        var supportedVideos = new[] { ".mp4", ".avi", ".wmv", ".mov", ".mkv" };

        mediaFiles = Directory.GetFiles(assetsPath)
                              .Where(f => supportedImages.Contains(Path.GetExtension(f).ToLower()) ||
                                          supportedVideos.Contains(Path.GetExtension(f).ToLower()))
                              .ToArray();

            // Console.WriteLine($"Number of media files found: {mediaFiles.Length}");

            imageTimer = new DispatcherTimer
            {
                Interval = displayDuration
            };
            imageTimer.Tick += ImageTimer_Tick;

        PlayMedia();
    }

    private void PlayMedia()
    {
        if (mediaFiles.Length == 0)
        {
            Console.WriteLine("No media files found.");
            return;
        }

        string currentFile = mediaFiles[currentIndex];
        string extension = Path.GetExtension(currentFile).ToLower();

        if (IsImageFile(extension))
        {
            imageElement.Visibility = Visibility.Visible;
            mediaElement.Visibility = Visibility.Collapsed;
            BitmapImage bitmap = new BitmapImage(new Uri(currentFile));
            imageElement.Source = bitmap;
            imageTimer.Start();
        }
        else if (IsVideoFile(extension))
        {
            imageElement.Visibility = Visibility.Collapsed;
            mediaElement.Visibility = Visibility.Visible;
            mediaElement.Source = new Uri(currentFile);
            mediaElement.Play();
        }
    }
    private void ImageTimer_Tick(object sender, EventArgs e)
    {
        imageTimer.Stop();
        currentIndex = (currentIndex + 1) % mediaFiles.Length;
        PlayMedia();
    }

    private void MediaElementEnd(object sender, RoutedEventArgs e)
    {
        currentIndex = (currentIndex + 1) % mediaFiles.Length;
        PlayMedia();
    }
    private static bool IsImageFile(string extension)
    {
        var supportedImages = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
        return supportedImages.Contains(extension);
    }
    private static bool IsVideoFile(string extension)
    {
        var supportedVideos = new[] { ".mp4", ".avi", ".wmv", ".mov", ".mkv" };
        return supportedVideos.Contains(extension);
    }
}
}

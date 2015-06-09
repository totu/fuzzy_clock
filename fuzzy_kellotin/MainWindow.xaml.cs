using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace fuzzy_kellotin
{
  public partial class MainWindow : Window
  {
    public static bool black { get; set; }
    private string APIKEY = "8e529d665a536bec7a86e55e82b64369";
    private string LOCALE = "Riihimäki,fi";
    
    public MainWindow()
    {
      InitializeComponent();
      black = true;
      Clock clock = new Clock(kello);
      Weather weather = new Weather(lampotila, image, LOCALE, APIKEY);
      clock.start();
      weather.start();
    }

    private void toggleColors()
    {
      if (black == true)
      {
        kello.Foreground = new SolidColorBrush(Colors.White);
        black = false;
      }
      else
      {
        kello.Foreground = new SolidColorBrush(Colors.Black);
        black = true;
      }
      lampotila.Foreground = kello.Foreground;
    }

    private void kello_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.ChangedButton == MouseButton.Left && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.LeftAlt)))
        this.DragMove();
    }

    private void kello_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.LeftAlt))
        toggleColors();
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
      if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.Space))
        toggleColors();
    }
  }
}

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
    private launcher launcher;

    public MainWindow()
    {
      InitializeComponent();
      launcher = new launcher();
      HotKey _hotKey = new HotKey(Key.Space, KeyModifier.Alt, OnHotKeyHandler);
      HotKey _hotKey2 = new HotKey(Key.Escape, KeyModifier.None, OnHotKeyHandler2);
      black = true;
      Clock clock = new Clock(kello);
      Weather weather = new Weather(lampotila, image, LOCALE, APIKEY);
      clock.start();
      weather.start();
    }

    private void OnHotKeyHandler(HotKey hotKey)
    {
      launcher.Show();
      launcher.Activate();
      launcher.searchBox.Focus();
    }

    private void OnHotKeyHandler2(HotKey hotKey)
    {
      launcher.Hide();
      launcher.test.Text = "";
      launcher.searchBox.Text = "";
      launcher.found = null;
      launcher.image.Source = null;
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

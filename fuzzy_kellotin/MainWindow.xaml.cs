using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;
using System.Windows.Media.Effects;

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
      black = true;
      Clock clock = new Clock(kello);
      Weather weather = new Weather(lampotila, image, LOCALE, APIKEY);
      clock.start();
      weather.start();
      checkSettings();
    }

    private void checkSettings()
    {
      string path = "fuzzy.json";
      if (File.Exists(path))
      {
        string settings = File.ReadAllText(path);
        dynamic obj = JsonConvert.DeserializeObject(settings);
        if (obj.color == "white")
        {
          toggleColors();
        }
      } else
      {
        string defaults = "{'color':'black'}";
        File.WriteAllText(path, defaults);
      }
    }

    private void OnHotKeyHandler(HotKey hotKey)
    {
      launcher.Show();
      launcher.Activate();
      launcher.searchBox.Focus();
      launcher.checkColors();
    }

    private void toggleColors()
    {
      string path = "fuzzy.json";
      string settings;

      DropShadowEffect dropShadow = new DropShadowEffect
      {
        Color = Colors.Black,
        Direction = 320,
        ShadowDepth = 0,
        Opacity = 1
      };

      if (black == true)
      {
        kello.Foreground = new SolidColorBrush(Colors.White);
        black = false;
        settings = "{'color':'white'}";
      }
      else
      {
        kello.Foreground = new SolidColorBrush(Colors.Black);
        dropShadow.Color = Colors.White;
        black = true;
        settings = "{'color':'black'}";
      }
      lampotila.Foreground = kello.Foreground;
      kello.Effect = dropShadow;
      lampotila.Effect = kello.Effect;
      File.WriteAllText(path, settings);
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

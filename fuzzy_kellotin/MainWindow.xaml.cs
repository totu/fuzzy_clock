using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace fuzzy_kellotin
{
  public partial class MainWindow : Window
  {
    private bool black = true;
    private string APIKEY = "";
    
    public MainWindow()
    {
      InitializeComponent();
      Clock clock = new Clock(kello);
      Weather weather = new Weather(lampotila, APIKEY);
      clock.start();
      weather.start();
    }

    private void kello_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.ChangedButton == MouseButton.Left && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.LeftAlt)))
        this.DragMove();
    }

    private void kello_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.LeftAlt))
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
  }
}

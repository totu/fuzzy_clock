using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace fuzzy_kellotin
{
  class Fader
  {
    private bool showing = true;
    private double c = 0.0;
    private DispatcherTimer drawer = new DispatcherTimer();
    private string previous = null;
    private Label label;
    private Image image;

    public Fader(Label obj)
    {
      label = obj;
      setTimer();
    }

    public Fader(Label obj, Image img)
    {
      label = obj;
      image = img;
      setTimer();
    }

    private void setTimer()
    {
      drawer.Tick += drawTick;
      drawer.Interval = new TimeSpan(0, 0, 0, 0, 50);
    }

    public void animate(string s)
    {
      if (previous == null || s != previous)
      {
        previous = s;
        drawer.Start();
      }
    }

    private void drawTick(object sender, EventArgs e)
    {
      if (showing == false)
      {
        c = c + 0.1;
        if (c >= 1)
        {
          showing = true;
          drawer.Stop();
        }
      }
      else
      {
        c = c - 0.1;
        if (c <= 0)
        {
          showing = false;
          if (label != null)
            label.Content = previous;
        }
      }
      if (label != null)
        label.Opacity = c;

      if (image != null)
        image.Opacity = c;
    }
  }
}

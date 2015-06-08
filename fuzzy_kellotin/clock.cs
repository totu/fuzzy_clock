using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace fuzzy_kellotin
{
  class Clock
  {
    private Label clock;
    private DispatcherTimer timer = new DispatcherTimer();
    private DispatcherTimer drawer = new DispatcherTimer();
    private bool showing = true;
    private double c = 1.0;
    private Fuzzier fuzz;
    private int hours;
    private int minutes;
    private string previous = null;

    public Clock(Label c)
    {
      clock = c;
      timer.Tick += timerTick;
      timer.Interval = new TimeSpan(0, 1, 0);
      fuzz = new Fuzzier();
      drawer.Tick += drawTick;
      drawer.Interval = new TimeSpan(0, 0, 0, 0, 50);
    }

    public void start()
    {
      getTime();
      timer.Start();
    }

    private void timerTick(object sender, EventArgs e)
    {
      getTime();
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
          clock.Content = previous;
        }
      }
      clock.Opacity = c;
    }

    private void getTime()
    {
      DateTime time = DateTime.Now;
      minutes = time.Minute;
      hours = time.Hour;
      string fuzzyTime = fuzz.FuzzyUp(hours, minutes);

      if (previous == null || fuzzyTime != previous)
      {
        previous = fuzzyTime;
        drawer.Start();
      }
    }
  }
}

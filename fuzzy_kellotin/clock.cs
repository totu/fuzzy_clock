using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace fuzzy_kellotin
{
  class Clock
  {
    private Label clock;
    private DispatcherTimer timer;
    private Fuzzier fuzz;
    private int hours;
    private int minutes;

    public Clock(Label c)
    {
      clock = c;
      timer = new DispatcherTimer();
      timer.Tick += timerTick;
      timer.Interval = new TimeSpan(0, 0, 1);
      fuzz = new Fuzzier();
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

    private void getTime()
    {
      DateTime time = DateTime.Now;
      minutes = time.Minute;
      hours = time.Hour;
      clock.Content = fuzz.FuzzyUp(hours, minutes);
    }
  }
}

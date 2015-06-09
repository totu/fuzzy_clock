using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace fuzzy_kellotin
{
  class Clock : Fader
  {
    private DispatcherTimer timer = new DispatcherTimer();
    private Fuzzier fuzz;
    private int hours;
    private int minutes;
    private string previous = null;

    public Clock(Label obj) : base(obj)
    {
      timer.Tick += timerTick;
      timer.Interval = new TimeSpan(0, 1, 0);
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
      string fuzzyTime = fuzz.FuzzyUp(hours, minutes);
      animate(fuzzyTime);
    }
  }
}

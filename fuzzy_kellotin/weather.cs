using Newtonsoft.Json;
using System;
using System.Net;
using System.Windows.Controls;
using System.Windows.Threading;

namespace fuzzy_kellotin
{
  class Weather
  {
    private string APIKEY;
    private string LOCALE;
    private Label weather;
    private DispatcherTimer timer;

    public Weather(Label w, string locale, string apikey)
    {
      weather = w;
      APIKEY = apikey;
      LOCALE = locale;
      timer = new DispatcherTimer();
      timer.Tick += timerTick;
      timer.Interval = new TimeSpan(0, 30, 0);
    }
    
    private void timerTick(object sender, EventArgs e)
    {
      getWeather();
    }
    
    public void start()
    {
      getWeather();
      timer.Start();
    }

    private void getWeather()
    {
      weather.Content = getWeatherFromAPI();
    }

    private string getWeatherFromAPI()
    {
      string tempeture = "API error";
      using (var webClient = new System.Net.WebClient())
      {
        var js = new WebClient().DownloadString("http://api.openweathermap.org/data/2.5/weather?q=" + LOCALE + "&APIID=" + APIKEY);
        dynamic json = JsonConvert.DeserializeObject(js);
        tempeture = Convert.ToString(Math.Round((Convert.ToDouble(json.main.temp) - 273.15))) + " °C";
      }
      return tempeture;
    }
  }
}

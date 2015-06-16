using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace fuzzy_kellotin
{
  class Weather : Fader
  {
    private string APIKEY;
    private string LOCALE;
    private DispatcherTimer timer;
    private Image image;
    private string weatherDescription = "";

    public Weather(Label obj, Image img, string locale, string apikey) : base(obj, img)
    {
      image = img;
      APIKEY = apikey;
      LOCALE = locale;
      timer = new DispatcherTimer();
      timer.Tick += timerTick;
      timer.Interval = new TimeSpan(0, 1, 0);
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

    private BitmapImage setWeatherIcon(string s)
    {
      string color = "w";
      if (MainWindow.black == true) color = "b";
      Uri uri = new Uri(@"/fuzzy_kellotin;component/" + s + "_" + color + ".png", UriKind.Relative);

      BitmapImage i = new BitmapImage();
      i.BeginInit();
      i.UriSource = uri;
      i.EndInit();

      return i;
    }

    private void getWeather()
    {
      string weather = getWeatherFromAPI();
      image.Source = setWeatherIcon(weatherDescription);
      if (weather != "API error")
        animate(weather);
    }

    private string getWeatherFromAPI()
    {
      string tempeture;
      try
      {
        using (var webClient = new System.Net.WebClient())
        {
          string pattern = @"rain|snow";
          Regex r = new Regex(pattern);
          MatchCollection m;
          var js = new WebClient().DownloadString("http://api.openweathermap.org/data/2.5/weather?q=" + LOCALE + "&APIID=" + APIKEY);
          dynamic json = JsonConvert.DeserializeObject(js);
          tempeture = Convert.ToString(Math.Round((Convert.ToDouble(json.main.temp) - 273.15))) + " °C";

          JArray a = json.weather;
          string description = a[0]["main"].ToString();
          m = r.Matches(description);
          if (m.Count > 0)
            weatherDescription = m[0].Value;
        }
      } catch
      {
        tempeture = "API error";
      }
      return tempeture;
    }
  }
}

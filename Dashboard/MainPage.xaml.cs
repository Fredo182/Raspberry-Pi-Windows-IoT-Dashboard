using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml.Linq;
using Dashboard.Classes;
using System.Dynamic;
using System.Runtime.Serialization.Json;
using Dashboard.Models;
using Windows.ApplicationModel;
using Windows.Storage;
using System.Globalization;




// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Dashboard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            GetWeather();
        }


        private async void GetWeather()
        {
            YahooWeatherResponse rsp = await YahooWeather.getWeather();

            loadPage(rsp.query.results.data);
            loadRadarMap(rsp.query.results.data.weather.latitude, rsp.query.results.data.weather.longitude);



        }

        private void loadPage(YahooWeatherData data)
        {
            loadCurrent(data);
            loadForecast(data);

        }
        private async void loadRadarMap(string lat, string lon)
        {
            StorageFile file = await Package.Current.InstalledLocation.GetFileAsync(@"Html\map.html");
            string html = await FileIO.ReadTextAsync(file);
            html = html.Replace("[LAT]", lat).Replace("[LON]", lon);
            this.radarMap.NavigateToString(html);
        }

        private void loadCurrent(YahooWeatherData data)
        {
            DateTime currentDate = TrimZoneAndParse(data.weather.date);
            this.WeatherCurrentDate.Text = String.Format("{0:dddd MMMM d, yyyy}", currentDate);

            this.WeatherCity.Text = data.location.city;
            this.WeatherCurrentCode.Text = YahooWeatherCodes.getString(data.weather.condition.code);
            this.WeatherCurrentCondition.Text = data.weather.condition.text;
            this.WeatherCurrentTemp.Text = data.weather.condition.temp + "°" + data.units.temperature;

            this.WeatherChill.Text = data.wind.chill + "°" + data.units.temperature;
            this.WeatherWind.Text = data.wind.speed + data.units.speed;
            this.WeatherHumidity.Text = data.atmosphere.humidity + "%";
            this.WeatherPressure.Text = data.atmosphere.pressure + " " + data.units.pressure;
            this.WeatherSunrise.Text = data.astronomy.sunrise;
            this.WeatherSunset.Text = data.astronomy.sunset;

            DateTime updatedTime = TrimZoneAndParse(data.weather.condition.date);
            this.WeatherUpdatedTime.Text = String.Format("{0:h:mm tt}", updatedTime).ToLower();

        }

        private void loadForecast(YahooWeatherData data)
        {
            var forecast = data.weather.forecast;

            DateTime day1 = ParseForecastDate(forecast[0].date);
            this.ForecastDay1.Text = String.Format("{0:dddd}", day1);
            this.ForecastCode1.Text = YahooWeatherCodes.getString(forecast[0].code);
            this.ForecastCondition1.Text = forecast[0].text;
            this.ForecastHigh1.Text = forecast[0].high + "°" + data.units.temperature;
            this.ForecastLow1.Text = forecast[0].low + "°" + data.units.temperature;

            DateTime day2 = ParseForecastDate(forecast[1].date);
            this.ForecastDay2.Text = String.Format("{0:dddd}", day2);
            this.ForecastCode2.Text = YahooWeatherCodes.getString(forecast[1].code);
            this.ForecastCondition2.Text = forecast[1].text;
            this.ForecastHigh2.Text = forecast[1].high + "°" + data.units.temperature;
            this.ForecastLow2.Text = forecast[1].low + "°" + data.units.temperature;

            DateTime day3 = ParseForecastDate(forecast[2].date);
            this.ForecastDay3.Text = String.Format("{0:dddd}", day3);
            this.ForecastCode3.Text = YahooWeatherCodes.getString(forecast[2].code);
            this.ForecastCondition3.Text = forecast[2].text;
            this.ForecastHigh3.Text = forecast[2].high + "°" + data.units.temperature;
            this.ForecastLow3.Text = forecast[2].low + "°" + data.units.temperature;

            DateTime day4 = ParseForecastDate(forecast[3].date);
            this.ForecastDay4.Text = String.Format("{0:dddd}", day4);
            this.ForecastCode4.Text = YahooWeatherCodes.getString(forecast[3].code);
            this.ForecastCondition4.Text = forecast[3].text;
            this.ForecastHigh4.Text = forecast[3].high + "°" + data.units.temperature;
            this.ForecastLow4.Text = forecast[3].low + "°" + data.units.temperature;

            DateTime day5 = ParseForecastDate(forecast[4].date);
            this.ForecastDay5.Text = String.Format("{0:dddd}", day5);
            this.ForecastCode5.Text = YahooWeatherCodes.getString(forecast[4].code);
            this.ForecastCondition5.Text = forecast[4].text;
            this.ForecastHigh5.Text = forecast[4].high + "°" + data.units.temperature;
            this.ForecastLow5.Text = forecast[4].low + "°" + data.units.temperature;

            DateTime day6 = ParseForecastDate(forecast[5].date);
            this.ForecastDay6.Text = String.Format("{0:dddd}", day6);
            this.ForecastCode6.Text = YahooWeatherCodes.getString(forecast[5].code);
            this.ForecastCondition6.Text = forecast[5].text;
            this.ForecastHigh6.Text = forecast[5].high + "°" + data.units.temperature;
            this.ForecastLow6.Text = forecast[5].low + "°" + data.units.temperature;

            DateTime day7 = ParseForecastDate(forecast[6].date);
            this.ForecastDay7.Text = String.Format("{0:dddd}", day7);
            this.ForecastCode7.Text = YahooWeatherCodes.getString(forecast[6].code);
            this.ForecastCondition7.Text = forecast[6].text;
            this.ForecastHigh7.Text = forecast[6].high + "°" + data.units.temperature;
            this.ForecastLow7.Text = forecast[6].low + "°" + data.units.temperature;


        }

        private static DateTime TrimZoneAndParse(string time)
        {
            int lastSpace = time.LastIndexOf(' ');
            if (lastSpace != -1)
            {
                time = time.Substring(0, lastSpace);
            }
            return DateTime.ParseExact(time,
                "ddd, dd MMM yyyy h:mm tt",
                CultureInfo.InvariantCulture);
        }

        private static DateTime ParseForecastDate(string time)
        {
            return DateTime.ParseExact(time,
                "dd MMM yyyy",
                CultureInfo.InvariantCulture);
        }




    }
}

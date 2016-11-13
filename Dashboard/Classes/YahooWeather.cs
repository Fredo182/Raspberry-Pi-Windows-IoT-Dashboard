using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dashboard.Classes
{
    [DataContract]
    class Location
    {
        [DataMember(Name = "city", IsRequired = true)]
        public string city;
        [DataMember(Name = "region", IsRequired = true)]
        public string region;
        [DataMember(Name = "country", IsRequired = true)]
        public string country;

    }

    [DataContract]
    class Units
    {
        [DataMember(Name = "distance", IsRequired = true)]
        public string distance;
        [DataMember(Name = "pressure", IsRequired = true)]
        public string pressure;
        [DataMember(Name = "speed", IsRequired = true)]
        public string speed;
        [DataMember(Name = "temperature", IsRequired = true)]
        public string temperature;
    }

    [DataContract]
    class Wind
    {
        [DataMember(Name = "chill", IsRequired = true)]
        public string chill;
        [DataMember(Name = "direction", IsRequired = true)]
        public string direction;
        [DataMember(Name = "speed", IsRequired = true)]
        public string speed;
    }

    [DataContract]
    class Atmostphere
    {
        [DataMember(Name = "humidity", IsRequired = true)]
        public string humidity;
        [DataMember(Name = "pressure", IsRequired = true)]
        public string pressure;
        [DataMember(Name = "rising", IsRequired = true)]
        public string rising;
        [DataMember(Name = "visibility", IsRequired = true)]
        public string visibility;
    }

    [DataContract]
    class Astronomy
    {
        [DataMember(Name = "sunrise", IsRequired = true)]
        public string sunrise;
        [DataMember(Name = "sunset", IsRequired = true)]
        public string sunset;
    }

    [DataContract]
    class CurrentWeather
    {
        [DataMember(Name = "code", IsRequired = true)]
        public string code;
        [DataMember(Name = "date", IsRequired = true)]
        public string date;
        [DataMember(Name = "temp", IsRequired = true)]
        public string temp;
        [DataMember(Name = "text", IsRequired = true)]
        public string text;
    }

    [DataContract]
    class Forecast
    {
        [DataMember(Name = "code", IsRequired = true)]
        public string code;
        [DataMember(Name = "date", IsRequired = true)]
        public string date;
        [DataMember(Name = "day", IsRequired = true)]
        public string day;
        [DataMember(Name = "high", IsRequired = true)]
        public string high;
        [DataMember(Name = "low", IsRequired = true)]
        public string low;
        [DataMember(Name = "text", IsRequired = true)]
        public string text;

    }

    [DataContract]
    class Weather
    {
        [DataMember(Name = "lat", IsRequired = true)]
        public string latitude;

        [DataMember(Name = "long", IsRequired = true)]
        public string longitude;

        [DataMember(Name = "condition", IsRequired = true)]
        public CurrentWeather condition;

        [DataMember(Name = "forecast", IsRequired = true)]
        public List<Forecast> forecast;

        [DataMember(Name = "pubDate", IsRequired = true)]
        public string date;

    }

    [DataContract]
    class YahooData
    {
        [DataMember(Name = "units", IsRequired = true)]
        public Units units;

        [DataMember(Name = "location", IsRequired = true)]
        public Location location;

        [DataMember(Name = "wind", IsRequired = true)]
        public Wind wind;

        [DataMember(Name = "atmosphere", IsRequired = true)]
        public Atmostphere atmosphere;

        [DataMember(Name = "astronomy", IsRequired = true)]
        public Astronomy astronomy;

        [DataMember(Name = "item", IsRequired = true)]
        public Weather weather;

    }

    [DataContract]
    class YahooWeatherResult
    {
        [DataMember(Name = "channel", IsRequired = true)]
        public YahooData data;

    }

    [DataContract]
    class YahooWeatherQuery
    {

        [DataMember(Name = "results", IsRequired = true)]
        public YahooWeatherResult results;

        [DataMember(Name = "count", IsRequired = true)]
        public Int16 count;

        [DataMember(Name = "created", IsRequired = true)]
        public String created;

        [DataMember(Name = "lang", IsRequired = true)]
        public String lang;
    }

    [DataContract]
    class YahooWeatherResponse
    {
        [DataMember(Name = "query", IsRequired = true)]
        public YahooWeatherQuery query;

    }

    class YahooWeather
    {
        public static async Task<YahooWeatherResponse> getWeather()
        {
            try
            {
                string baseUrl = "http://query.yahooapis.com/v1/public/yql";
                string yqlQuery = "select * from weather.forecast where woeid in (select woeid from geo.places(1) where text='chicago, il')";
                string yqlQueryUrl = baseUrl + "?q=" + Uri.EscapeDataString(yqlQuery) + "&format=json";
                var client = new HttpClient();
                var response = await client.GetAsync(yqlQueryUrl);
                response.EnsureSuccessStatusCode();
                string rsp = await response.Content.ReadAsStringAsync();


                MemoryStream stream1 = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(rsp));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(YahooWeatherResponse));
                YahooWeatherResponse weather = (YahooWeatherResponse)ser.ReadObject(stream1);
                return weather;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

    }




}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dashboard.Models
{
    class Location
    {
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
    class Units
    {
        public string distance { get; set; }
        public string pressure { get; set; }
        public string speed { get; set; }
        public string temperature { get; set; }
    }
    class Wind
    {
        public string chill { get; set; }
        public string direction { get; set; }
        public string speed { get; set; }
    }

    class Atmostphere
    {
        public string humidity { get; set; }
        public string pressure { get; set; }
        public string rising { get; set; }
        public string visibility { get; set; }
    }

    class Astronomy
    {
        public string sunrise { get; set; }
        public string sunset { get; set; }
    }
    class CurrentWeather
    {
        
        public string code { get; set; }
        public string date { get; set; }
        public string temp { get; set; }
        public string text { get; set; }
    }

    class Forecast
    {
        public string code { get; set; }
        public string date { get; set; }
        public string day { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string text { get; set; }
    }

    class Weather
    {

        public Location Location {get; set;}
        public Units Units { get; set; }
        public Wind Wind { get; set; }
        public Atmostphere Atmosphere { get; set; }
        public Astronomy Astronomy { get; set; }
        public CurrentWeather CurrentWeather { get; set; }
        public List<Forecast> Forecast { get; set; }
        public string LastUpdated { get; set; }

        public Weather(XDocument weather)
        {
            XNamespace ns = "http://xml.weather.yahoo.com/ns/rss/1.0";
            XNamespace geons = "http://www.w3.org/2003/01/geo/wgs84_pos#";

            // Set the location
            this.Location = new Location();
            XElement locationElement = weather.Descendants(ns + "location").First();
            this.Location.city = locationElement.Attribute("city").Value;
            this.Location.region = locationElement.Attribute("region").Value;
            this.Location.country = locationElement.Attribute("country").Value;
            XElement geoLatElement = weather.Descendants(geons + "lat").First();
            XElement geoLongElement = weather.Descendants(geons + "long").First();
            this.Location.latitude = geoLatElement.Value;
            this.Location.longitude = geoLongElement.Value;

            // Set the units
            this.Units = new Units();
            XElement unitsElement = weather.Descendants(ns + "units").First();
            this.Units.distance = unitsElement.Attribute("distance").Value;
            this.Units.pressure = unitsElement.Attribute("pressure").Value;
            this.Units.speed = unitsElement.Attribute("speed").Value;
            this.Units.temperature = unitsElement.Attribute("temperature").Value;

            // Set the wind
            this.Wind = new Wind();
            XElement windElement = weather.Descendants(ns + "wind").First();
            this.Wind.chill = windElement.Attribute("chill").Value;
            this.Wind.direction = windElement.Attribute("direction").Value;
            this.Wind.speed = windElement.Attribute("speed").Value;

            // Set the atmosphere
            this.Atmosphere = new Atmostphere();
            XElement atmosphereElement = weather.Descendants(ns + "atmosphere").First();
            this.Atmosphere.humidity = atmosphereElement.Attribute("humidity").Value;
            this.Atmosphere.pressure = atmosphereElement.Attribute("pressure").Value;
            this.Atmosphere.rising = atmosphereElement.Attribute("rising").Value;
            this.Atmosphere.visibility = atmosphereElement.Attribute("visibility").Value;

            // Set the astronomy
            this.Astronomy = new Astronomy();
            XElement astronomyElement = weather.Descendants(ns + "astronomy").First();
            this.Astronomy.sunrise = astronomyElement.Attribute("sunrise").Value;
            this.Astronomy.sunset = astronomyElement.Attribute("sunset").Value;

            // Set the current condition
            this.CurrentWeather = new CurrentWeather();
            XElement currentWeatherElement = weather.Descendants(ns + "condition").First();
            this.CurrentWeather.code = currentWeatherElement.Attribute("code").Value;
            this.CurrentWeather.date = currentWeatherElement.Attribute("date").Value;
            this.CurrentWeather.temp = currentWeatherElement.Attribute("temp").Value;
            this.CurrentWeather.text = currentWeatherElement.Attribute("text").Value;

            // Set the forecast
            IEnumerable<XElement> forecastElements = weather.Descendants(ns + "forecast");
            this.Forecast = new List<Forecast>();
            foreach(XElement element in forecastElements)
            {
                var forecast = new Forecast();
                forecast.code = element.Attribute("code").Value;
                forecast.date = element.Attribute("date").Value;
                forecast.day = element.Attribute("day").Value;
                forecast.high = element.Attribute("high").Value;
                forecast.low = element.Attribute("low").Value;
                forecast.text = element.Attribute("text").Value;
                this.Forecast.Add(forecast);
            }

            // Set the updated time
            XElement lastUpdatedElement = weather.Descendants("lastBuildDate").First();
            this.LastUpdated = lastUpdatedElement.Value;
            

        }


    }

   
}

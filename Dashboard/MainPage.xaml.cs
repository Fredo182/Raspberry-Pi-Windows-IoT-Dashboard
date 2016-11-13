﻿using System;
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

            //this.test.Text = YahooWeatherCodes.getString(rsp.query.results.data.weather.condition.code);

            Debug.WriteLine(rsp.query.results.data.location.city);



        }


    }
}
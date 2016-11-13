using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Classes
{
    class YahooWeatherCodes
    {
        //http://erikflowers.github.io/weather-icons/
        //http://erikflowers.github.io/weather-icons/api-list.html

        static int[] codes = new int[]
        {
            0xf056,
            0xf00e,
            0xf073,
            0xf01e,
            0xf01e,
            0xf017,
            0xf0b5,
            0xf0b5,
            0xf015,
            0xf04e,
            0xf015,
            0xf01a,
            0xf01a,
            0xf01b,
            0xf01b,
            0xf064,
            0xf01b,
            0xf015,
            0xf0b5,
            0xf063,
            0xf014,
            0xf0b6,
            0xf062,
            0xf050,
            0xf021,
            0xf076,
            0xf013,
            0xf086,
            0xf002,
            0xf083,
            0xf00c,
            0xf02e,
            0xf00d,
            0xf02e,
            0xf00d,
            0xf015,
            0xf072,
            0xf01e,
            0xf01e,
            0xf01e,
            0xf01a,
            0xf01b,
            0xf01b,
            0xf01b,
            0xf013,
            0xf01e,
            0xf01b,
            0xf01d,
            0xf07b
        };

        public static string getString(string code)
        {
            return Convert.ToString((char)codes[Convert.ToUInt32(code)]);
        }
    }
}

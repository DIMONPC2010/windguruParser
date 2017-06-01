using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindguruParser
{
    class Weather
    {
        public int Id { get; set; }
        public double temperature { get; set; }
        public double gust { get; set; }
        public double windspd { get; set; }
        public String winddir { get; set; }
        public String weekday { get; set; }
        public DateTime date { get; set; }

        public override string ToString()
        {
            return (temperature.ToString() + " " +
                    gust.ToString() + " " +
                    windspd.ToString() + " " +
                    winddir + " " +
                    weekday + " " +
                    date.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WindguruParser
{
    [Serializable]
    public class WindData
    {
        public string Email { get; set; }
        public string WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public string Gust { get; set; }

        public WindData()
        { }

        public WindData(string email, string windspeed,string winddirection,string gust)
        {
            Email = email;
            WindDirection=winddirection;
            WindSpeed = windspeed;
            Gust = gust;
        }

        public int WindSpeedMin()
        {
            string s = WindSpeed.Remove(WindSpeed.IndexOf("-"));
            int windSPD = Convert.ToInt32(s);
            return windSPD;
        }

        public int WindSpeedMax()
        {
            string s = WindSpeed.Remove(0, WindSpeed.IndexOf("-")+1);
            int windSPD = Convert.ToInt32(s);
            return windSPD;
        }

        public int GustMin()
        {
            string s = Gust.Remove(Gust.IndexOf("-"));
            int gust = Convert.ToInt32(s);
            return gust;
        }

        public int GustMax()
        {
            string s = Gust.Remove(0, Gust.IndexOf("-") + 1);
            int gust = Convert.ToInt32(s);
            return gust;
        }
    }
}

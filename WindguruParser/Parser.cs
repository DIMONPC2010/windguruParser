using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace WindguruParser
{
    class Parser
    {
        static private String ConvertToString(String s)
        {
            if (s == "null")
                s = "0";
            int windDIR = Convert.ToInt32(s);

            switch (windDIR)
            {
                case 0:
                    s = "N";
                    break;
                case 45:
                    s = "NE";
                    break;
                case 90:
                    s = "E";
                    break;
                case 135:
                    s = "SE";
                    break;
                case 180:
                    s = "S";
                    break;
                case 225:
                    s = "SW";
                    break;
                case 270:
                    s = "W";
                    break;
                case 315:
                    s = "NW";
                    break;
            }

            if (windDIR > 0 && windDIR < 45)
                s = "NNE";

            if (windDIR > 45 && windDIR < 90)
                s = "ENE";

            if (windDIR > 90 && windDIR < 135)
                s = "ESE";

            if (windDIR > 135 && windDIR < 180)
                s = "SSE";

            if (windDIR > 180 && windDIR < 225)
                s = "SSW";

            if (windDIR > 225 && windDIR < 270)
                s = "WSW";

            if (windDIR > 270 && windDIR < 315)
                s = "WNW";

            if (windDIR > 315 && windDIR <= 359)
                s = "NNW";

            return s;
        }
        static public List<Weather> ParseWeather(ref DateTime updateNext)
        {
            HttpWebRequest req = WebRequest.Create("https://www.windguru.cz/fcst.php?s=87721&") as HttpWebRequest;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            StreamReader stream = new StreamReader(resp.GetResponseStream());
            String responseString = stream.ReadToEnd();

            //Thread.Sleep(100);

            String data = responseString.Substring(responseString.IndexOf("\"TMP"));
            updateNext = DateTime.ParseExact(data.Substring(data.IndexOf("update_next") + 14, 19), "yyyy-MM-dd HH:mm:ss", null);
            //updateNext = new DateTime(updateNext.Year, updateNext.Month, updateNext.Day, updateNext.Hour + 3, updateNext.Minute, updateNext.Second);
            data = data.Remove(data.IndexOf("\"hours"));
            data = data.Remove(data.IndexOf("\"TCDC"), data.IndexOf("\"GUST") - data.IndexOf("\"TCDC"));
            data = data.Remove(data.IndexOf("\"SLP"), data.IndexOf("\"WINDSPD") - data.IndexOf("\"SLP"));
            data = data.Remove(data.IndexOf("\"SMERN"), data.IndexOf("\"hr_weekday") - data.IndexOf("\"SMERN"));

            List<Weather> weatherList = new List<Weather>();

            data = data.Remove(0, 7);

            int iter = 0;
            String substr = "";
            while (true)
            {
                if (data[iter] != ',' && data[iter] != ']')
                {
                    if (data[iter] == '.')
                        substr += ",";
                    else
                        substr += data[iter];
                }
                else
                {
                    if (substr == "null")
                        substr = "0";
                    Weather w = new Weather();
                    w.temperature = Convert.ToDouble(substr);
                    weatherList.Add(w);
                    substr = "";
                    if (data[iter] == ']')
                        break;
                }
                iter++;
            }

            for (int i = 0; i < weatherList.Count; i++)
            {
                weatherList[i].Id = i+1;
            }

            data = data.Remove(0, data.IndexOf("GUST") + 7);
            iter = 0;
            int weatherIter = 0;
            while (true)
            {
                if (data[iter] != ',' && data[iter] != ']')
                {
                    if (data[iter] == '.')
                        substr += ",";
                    else
                        substr += data[iter];
                }
                else
                {
                    if (substr == "null")
                        substr = "0";
                    weatherList[weatherIter].gust = Convert.ToDouble(substr);
                    weatherIter++;
                    substr = "";
                    if (data[iter] == ']')
                        break;
                }
                iter++;
            }

            data = data.Remove(0, data.IndexOf("WINDSPD") + 10);
            iter = 0;
            weatherIter = 0;
            while (true)
            {
                if (data[iter] != ',' && data[iter] != ']')
                {
                    if (data[iter] == '.')
                        substr += ",";
                    else
                        substr += data[iter];
                }
                else
                {
                    if (substr == "null")
                        substr = "0";
                    weatherList[weatherIter].windspd = Convert.ToDouble(substr);
                    weatherIter++;
                    substr = "";
                    if (data[iter] == ']')
                        break;
                }
                iter++;
            }

            data = data.Remove(0, data.IndexOf("WINDDIR") + 10);
            iter = 0;
            weatherIter = 0;
            while (true)
            {
                if (data[iter] != ',' && data[iter] != ']')
                {
                    if (data[iter] == '.')
                        substr += ",";
                    else
                        substr += data[iter];
                }
                else
                {
                    weatherList[weatherIter].winddir = ConvertToString(substr);
                    weatherIter++;
                    substr = "";
                    if (data[iter] == ']')
                        break;
                }
                iter++;
            }

            data = data.Remove(0, data.IndexOf("hr_weekday") + 13);
            iter = 0;
            weatherIter = 0;
            while (true)
            {
                if (data[iter] != ',' && data[iter] != ']')
                {
                    substr += data[iter];
                }
                else
                {
                    int tmp = Convert.ToInt32(substr);
                    switch (tmp)
                    {
                        case 0:
                            weatherList[weatherIter].weekday = "Sunday";
                            break;
                        case 1:
                            weatherList[weatherIter].weekday = "Monday";
                            break;
                        case 2:
                            weatherList[weatherIter].weekday = "Tuesday";
                            break;
                        case 3:
                            weatherList[weatherIter].weekday = "Wednesday";
                            break;
                        case 4:
                            weatherList[weatherIter].weekday = "Thursday";
                            break;
                        case 5:
                            weatherList[weatherIter].weekday = "Friday";
                            break;
                        case 6:
                            weatherList[weatherIter].weekday = "Saturday";
                            break;
                    }
                    weatherIter++;
                    substr = "";
                    if (data[iter] == ']')
                        break;
                }
                iter++;
            }

            data = data.Remove(0, data.IndexOf("hr_h") + 7);
            iter = 0;
            weatherIter = 0;

            while (true)
            {
                if (data[iter] != ',' && data[iter] != ']')
                {
                    substr += data[iter];
                }
                else
                {
                    substr = substr.Trim('"');
                    int tmp = Convert.ToInt32(substr);
                    weatherList[weatherIter].date = new DateTime(1, 1, 1, tmp, 0, 0);
                    weatherIter++;
                    substr = "";
                    if (data[iter] == ']')
                        break;
                }
                iter++;
            }

            data = data.Remove(0, data.IndexOf("hr_d") + 7);
            iter = 0;
            weatherIter = 0;

            while (true)
            {
                if (data[iter] != ',' && data[iter] != ']')
                {
                    substr += data[iter];
                }
                else
                {
                    substr = substr.Trim('"');
                    int tmp = Convert.ToInt32(substr);
                    int year = DateTime.Today.Year;
                    if (DateTime.Today.Month == 12 && DateTime.Today.Day == 31 && substr == "01")
                        year++;
                    int month = DateTime.Today.Month;
                    if ((DateTime.Today.Month == 8 || DateTime.Today.Month % 2 == 1) && DateTime.Today.Day > 22 && tmp < 31)
                        month = DateTime.Today.Month + 1;
                    if ((DateTime.Today.Month != 8 && DateTime.Today.Month % 2 == 0) && DateTime.Today.Day > 21 && tmp < 30)
                        month = DateTime.Today.Month + 1;
                    if (((DateTime.Today.Year % 4 == 0 && DateTime.Today.Year % 100 != 0) || (DateTime.Today.Year % 400 == 0)) &&
                          DateTime.Today.Month == 2 && DateTime.Today.Day > 20 && tmp < 29)
                        month = DateTime.Today.Month + 1;
                    if (DateTime.Today.Month == 2 && DateTime.Today.Day > 19 && tmp < 28)
                        month = DateTime.Today.Month + 1;
                    if((tmp==30||tmp==31)&& DateTime.Today.Day==1)
                        month = DateTime.Today.Month - 1;
                    weatherList[weatherIter].date = new DateTime(year, month, tmp, weatherList[weatherIter].date.Hour, 0, 0);
                    weatherIter++;
                    substr = "";
                    if (data[iter] == ']')
                        break;
                }
                iter++;
            }

            return weatherList;
        }
    }
}

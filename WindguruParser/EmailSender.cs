using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Net.Mail;

namespace WindguruParser
{
    class EmailSender
    {
        public Thread thread { get; private set; }

        public EmailSender(WindData wind)
        {
            thread = new Thread(this.send);
            thread.IsBackground = true;
            thread.Start(wind);
        }
        void send(object wind)
        {
            WindData windData = new WindData();
            int iter = 0;
            while (true)
            {
                SharedRes.mtx.WaitOne();
                if (DateTime.UtcNow > SharedRes.updateTime||iter==0)
                {
                    iter = 1;
                    windData = (WindData)wind;

                    DateTime nowDate = DateTime.Now;

                    String dbFileName = "WindguruDB"; ;
                    SQLiteConnection m_dbConn = new SQLiteConnection();
                    SQLiteCommand m_sqlCmd = new SQLiteCommand();

                    if (!File.Exists(dbFileName))
                        MessageBox.Show("Please, create DB and blank table");

                    try
                    {
                        m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                        m_dbConn.Open();
                        m_sqlCmd.Connection = m_dbConn;
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }

                    List<Weather> coolWeather = new List<Weather>();

                    if (m_dbConn.State != ConnectionState.Open)
                    {
                        MessageBox.Show("Open connection with database");
                        return;
                    }

                    try
                    {
                        m_sqlCmd = new SQLiteCommand("SELECT * FROM Catalog", m_dbConn);
                        SQLiteDataReader reader = m_sqlCmd.ExecuteReader();

                        foreach (DbDataRecord record in reader)
                        {
                            string date = record["Date"].ToString();
                            DateTime FutureDay = new DateTime();
                            FutureDay = DateTime.ParseExact(date, "dd.MM.yyyy H:mm:ss", null);
                            TimeSpan ts = new TimeSpan();
                            ts = FutureDay - DateTime.Now;
                            if (ts.Days == 1)
                            {
                                string windSPD = record["WindSpeed"].ToString();
                                string windDIR = record["WindDirection"].ToString();
                                string gust = record["Gust"].ToString();

                                int windSpeed = Convert.ToInt32(windSPD);
                                int Gust = Convert.ToInt32(gust);

                                if (windSpeed > windData.WindSpeedMin() && windSpeed < windData.WindSpeedMax() &&
                                    Gust > windData.GustMin() && Gust < windData.GustMax() &&
                                    windDIR == windData.WindDirection)
                                {
                                    Weather w = new Weather();
                                    w.date = FutureDay;
                                    w.windspd = windSpeed;
                                    w.gust = Gust;
                                    w.winddir = windDIR;
                                    coolWeather.Add(w);
                                }
                            }
                        }
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    m_dbConn.Close();

                    if (coolWeather.Count > 0)
                    {
                        string from1 = "dmytro.anshakov@nure.ua";
                        string mailto = windData.Email;
                        string caption = "WindguruParser";
                        string message = "";
                        string password = "F55v81aB";
                        for (int i = 0; i < coolWeather.Count; i++)
                        {
                            message += coolWeather[i].date.ToString() + ";   Wind speed: " + coolWeather[i].windspd +
                                "   Gust " + coolWeather[i].gust + ";   Wind direction: " + coolWeather[i].winddir + ".";
                        }
                        try
                        {
                            MailAddress from = new MailAddress(from1, caption);
                            MailAddress to = new MailAddress(mailto);
                            MailMessage m = new MailMessage(from, to);
                            m.Subject = caption;
                            m.Body = message;
                            m.IsBodyHtml = true;
                            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                            smtp.Credentials = new NetworkCredential(from1, password);
                            smtp.EnableSsl = true;
                            smtp.Send(m);

                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }

                    SharedRes.mtx.ReleaseMutex();
        }
                else
                {
                    Thread.Sleep(100);
                    SharedRes.mtx.ReleaseMutex();
                }
}
        }
    }
}

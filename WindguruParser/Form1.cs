using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace WindguruParser
{
    public partial class Form1 : Form
    {
        private String dbFileName;
        private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;
        private WindguruParser.DbInit db;
        private String WindSpeed;
        private String WindDirection;
        private String Gust;
        private String Email;
        private XmlSerializer formatter;
        private EmailSender sendEmail;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();
            dbFileName = "WindguruDB";
            db = new DbInit(dbFileName, m_dbConn, m_sqlCmd);
            Email = "";
            formatter = new XmlSerializer(typeof(WindData));

            if (File.Exists("wind.xml"))
            {
                using (FileStream fs = new FileStream("wind.xml", FileMode.OpenOrCreate))
                {
                    WindData wind = (WindData)formatter.Deserialize(fs);

                    WindSpeed = wind.WindSpeed;
                    WindDirection = wind.WindDirection;
                    Gust = wind.Gust;
                    Email = wind.Email;
                    
                }
                tbEmail.Text = Email;
                tbGust.Text = Gust;
                tbWindDIR.Text = WindDirection;
                tbWindSPD.Text = WindSpeed;
                btApply.PerformClick();
            }


        }

        private void btSave_Click(object sender, EventArgs e)
        {
            string s = tbEmail.Text;
            string pattern = "[^:,;,<,>,?,/,%,$,@,#,!,*,(,)]{5,20}[@]{1}[a-z]{1,10}[.]{1}[a-z]{2,3}";
            string target = " ";
            Regex regex = new Regex(pattern);
            string result = regex.Replace(s, target);
            if (result == " ")
                Email=s;
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            SharedRes.mtx.WaitOne();
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

            DataTable dTable = new DataTable();
            String sqlQuery;

            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Open connection with database");
                return;
            }

            try
            {
                sqlQuery = "SELECT * FROM Catalog";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    dgvWeatherViewer.Rows.Clear();

                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        //Win32.AllocConsole();
                        //Console.WriteLine(dTable.Rows[i].ItemArray);
                        dgvWeatherViewer.Rows.Add(dTable.Rows[i].ItemArray);
                    }

                }
                else
                    MessageBox.Show("Database is empty");
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            m_dbConn.Close();
            SharedRes.mtx.ReleaseMutex();
        }

        private void btApply_Click(object sender, EventArgs e)
        {
            WindDirection = "";
            string s = tbWindDIR.Text;
            string pattern = "[W,S,N,E]{1}";
            string target = " ";
            Regex regex = new Regex(pattern);
            string result = regex.Replace(s, target);
            if (result == " ")
                WindDirection=s;
            else
            {
                s = tbWindDIR.Text;
                pattern = "[S,N]{1}[E,W]{1}";
                target = " ";
                regex = new Regex(pattern);
                result = regex.Replace(s, target);
                if (result == " ")
                    WindDirection = s;
                else
                {
                    s = tbWindDIR.Text;
                    pattern = "[W,S,N,E]{1}[N,S]{1}[E,W]{1}";
                    target = " ";
                    regex = new Regex(pattern);
                    result = regex.Replace(s, target);
                    if (result == " ")
                        WindDirection = s;
                }
            }

            WindSpeed = "";
            s = tbWindSPD.Text;
            pattern = "[0-9]{1,2}-[0-9]{1,2}";
            target = " ";
            regex = new Regex(pattern);
            result = regex.Replace(s, target);
            if (result == " ")
                WindSpeed = s;

            Gust = "";
            s = tbGust.Text;
            pattern = "[0-9]{1,2}-[0-9]{1,2}";
            target = " ";
            regex = new Regex(pattern);
            result = regex.Replace(s, target);
            if (result == " ")
                Gust = s;

            if (WindDirection != "" && WindSpeed != "" && Gust != "" && Email != "")
            {
                WindData wind = new WindData(Email,WindSpeed,WindDirection,Gust);

                using (FileStream fs = new FileStream("wind.xml", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, wind);
                }


                sendEmail = new EmailSender(wind);
            }
            else
            {
                if (WindDirection == "")
                    MessageBox.Show("Enter Wind direction. (Example - \"NE\")");
                if (WindSpeed == "")
                    MessageBox.Show("Enter Wind direction. (Example - \"4-5\")");
                if (Gust == "")
                    MessageBox.Show("Enter Gust. (Example - \"4-5\")");
                if (Email == "")
                    MessageBox.Show("Enter Email. (Example - \"user@mail.ru\")");
            }

        }
     private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SharedRes.mtx.WaitOne();
            sendEmail.thread.Abort();
            db.thread.Abort();
            System.Environment.Exit(1);
            
        }
    }
    }


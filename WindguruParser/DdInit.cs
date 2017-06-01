﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace WindguruParser
{
    class DbInit
    {
        Thread thread;
        DbInitData dbInitData;
        public DbInit(String dbFileName, SQLiteConnection m_dbConn, SQLiteCommand m_sqlCmd)
        {
            thread = new Thread(this.InitDataBase);
            dbInitData = new DbInitData(dbFileName, m_dbConn, m_sqlCmd);
            thread.Start(dbInitData);//передача параметра в поток
        }
        private void InitDataBase(object dbInitData)
        {
            DateTime updateNext = new DateTime();
            List<Weather> wtr;
            while (true)
            {
                SharedRes.mtx.WaitOne();
                if (DateTime.UtcNow > updateNext)
                {
                    wtr = new List<Weather>();
                    wtr = Parser.ParseWeather(ref updateNext);
                    DbInitData data = (DbInitData)dbInitData;
                    SharedRes.updateTime = updateNext;

                    if (!File.Exists(data.dbFileName))
                        SQLiteConnection.CreateFile(data.dbFileName);

                    try
                    {
                        data.m_dbConn = new SQLiteConnection("Data Source=" + data.dbFileName + ";Version=3;");
                        data.m_dbConn.Open();
                        data.m_sqlCmd.Connection = data.m_dbConn;

                        data.m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Catalog (" +
                                                    "id INTEGER PRIMARY KEY," +
                                                    "Date TEXT," +
                                                    "Weekday TEXT," +
                                                    "Temperature REAL," +
                                                    "Gust REAL," +
                                                    "WindSpeed REAL," +
                                                    "WindDirection TEXT)";
                        data.m_sqlCmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }

                    if (data.m_dbConn.State != ConnectionState.Open)
                    {
                        MessageBox.Show("Open connection with database");
                        return;
                    }

                    try
                    {
                        data.m_sqlCmd.CommandText = "DELETE FROM Catalog";

                        data.m_sqlCmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }


                    try
                    {
                        for (int i = 0; i < wtr.Count; i++)
                        {
                            data.m_sqlCmd.CommandText = "INSERT INTO Catalog ('id', 'Date', 'Weekday'," +
                                                                              "'Temperature', 'Gust'," +
                                                                              "'WindSpeed', 'WindDirection')" +
                                                        "values ('" + wtr[i].Id + "','" + wtr[i].date.ToString() + "','" + wtr[i].weekday +
                                                                "','" + wtr[i].temperature + "','" + wtr[i].gust + "','" +
                                                                wtr[i].windspd + "','" + wtr[i].winddir + "')";
                            data.m_sqlCmd.ExecuteNonQuery();
                        }

                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }

                    data.m_dbConn.Close();
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
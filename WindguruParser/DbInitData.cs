using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace WindguruParser
{
    class DbInitData
    {
        public String dbFileName { get; set; }
        public SQLiteConnection m_dbConn { get; set; }
        public SQLiteCommand m_sqlCmd { get; set; }
        public DataGridView m_dgv { get; set; }
        public Label m_lb { get; set; }

        public DbInitData(String _dbFileName, SQLiteConnection _dbConn, SQLiteCommand _sqlCmd, DataGridView _dgv, Label _lb)
        {
            dbFileName = _dbFileName;
            m_dbConn = _dbConn;
            m_sqlCmd = _sqlCmd;
            m_dgv = _dgv;
            m_lb = _lb;
        }
    }
}

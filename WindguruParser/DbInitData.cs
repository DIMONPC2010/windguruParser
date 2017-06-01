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

        public DbInitData(String _dbFileName, SQLiteConnection _dbConn, SQLiteCommand _sqlCmd)
        {
            dbFileName = _dbFileName;
            m_dbConn = _dbConn;
            m_sqlCmd = _sqlCmd;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Scan
{
    class DBase
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @"DESKTOP-3DMSTDM\SQLEXPRESS";
            string database = "Pictures";
            string username = "sa";
            string password = "1234";
            return DBServer.GetDBConnection(datasource, database, username, password);
        }
    }
}

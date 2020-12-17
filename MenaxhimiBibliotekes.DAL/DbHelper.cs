using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace MenaxhimiBibliotekes.DAL
{
    class DbHelper
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionEndrit"].ConnectionString;

        public static SqlConnection GetConnection()
        {

            try
            {
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot connect to Data Base server, please contact your administrator", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static SqlCommand Command(SqlConnection connection, string cmdText, CommandType commandType)
        {
            try
            {
                SqlCommand command = new SqlCommand(cmdText, connection);
                command.CommandType = commandType;
                return command;
            }
            catch (Exception)
            {
                return null;
            }
        }

 


    }
}

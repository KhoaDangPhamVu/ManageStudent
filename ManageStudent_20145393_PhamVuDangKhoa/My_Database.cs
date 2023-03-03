using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace ManageStudent_20145393_PhamVuDangKhoa
{

    internal class My_Database
    {
        private MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=manage_student");

        //Function to get the connection
        public MySqlConnection GetConnection
        {
            get { return conn; }
        }

        //funtion to open the connection
        public void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        //funtion to open the connection
        public void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}

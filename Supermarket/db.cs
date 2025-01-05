using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; // Importing required namespaces for SQL Server connection
using System.Windows.Forms; // Importing namespace for Windows Forms functionalities

namespace Shop
{
    class db
    {
        // Connection string for connecting to the database
        public static String connstr = "server=localhost; database=shopdb; uid=root; pwd=\"\";";
        // SqlConnection object to manage the connection
        public static SqlConnection con = new SqlConnection(connstr);

        // Method to open the database connection
        public static void openconnection()
        {
            try
            {
                // Check if the connection state is closed
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    // Open the connection if it is closed
                    con.Open();
                }
            }
            catch (Exception e) // Exception handling for connection failures
            {
                // Show a message box if the database connection fails
                MessageBox.Show("Database Connection Failed");
                // Log the exception details to the console
                Console.WriteLine(e);
            }
        }
    }
}

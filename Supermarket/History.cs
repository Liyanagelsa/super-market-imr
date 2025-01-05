using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Shop
{
    public partial class History : Form
    {
        // Constructor for initializing the History form
        public History()
        {
            InitializeComponent();
        }

        // Event handler to close the application when label5 is clicked
        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // SQL connection string for connecting to the database
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-LBKQMDG;Initial Catalog=shopdb;Integrated Security=True;Encrypt=False;Connect Timeout=30");

        // Method to fetch data from HistoryTable and display it in SPPtable
        private void fetchDataHistory()
        {
            Con.Open();
            string query = "select * from HistoryTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var data = new DataSet();
            sda.Fill(data);
            SPPtable.DataSource = data.Tables[0];
            Con.Close();
        }

        // Method to fetch data from AllSalesTable and display it in AStable
        private void fetchDataAllSales()
        {
            Con.Open();
            string query = "select * from AllSalesTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var data = new DataSet();
            sda.Fill(data);
            AStable.DataSource = data.Tables[0];
            Con.Close();
        }

        // Event handler for form load to fetch initial data for History and AllSales
        private void History_Load(object sender, EventArgs e)
        {
            fetchDataHistory();
            fetchDataAllSales();
        }

        // Placeholder for button5 click event (currently no implementation)
        private void button5_Click(object sender, EventArgs e)
        {

        }

        // Method to fetch filtered data from HistoryTable based on search text
        private void fetchDataSpecificText()
        {
            Con.Open();
            string query = "select * from HistoryTable where AttName like '" + "%" + searchH.Text + "%" + "'" + "or" + " date like '" + "%" + searchH.Text + "%" + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var data = new DataSet();
            sda.Fill(data);
            SPPtable.DataSource = data.Tables[0];
            Con.Close();
        }

        // Method to fetch filtered data from AllSalesTable based on search text
        private void fetchDataSpecificText2()
        {
            Con.Open();
            string query = "select * from AllSalesTable where Date like '" + "%" + searchA.Text + "%" + "'" + "or" + " Name like '" + "%" + searchA.Text + "%" + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var data = new DataSet();
            sda.Fill(data);
            AStable.DataSource = data.Tables[0];
            Con.Close();
        }

        // Event handler for text change in description field to fetch specific data
        private void description_TextChanged(object sender, EventArgs e)
        {
            fetchDataSpecificText();
        }

        // Event handler for text change in searchA field to fetch specific data
        private void searchA_TextChanged(object sender, EventArgs e)
        {
            fetchDataSpecificText2();
        }

        // Event handler to refresh HistoryTable data and clear the search text
        private void refreshH_Click(object sender, EventArgs e)
        {
            fetchDataHistory();
            searchH.Text = "";
        }

        // Event handler to refresh AllSalesTable data and clear the search text
        private void refreshA_Click(object sender, EventArgs e)
        {
            fetchDataAllSales();
            searchA.Text = "";
        }

        // Placeholder for label3 click event (currently no implementation)
        private void label3_Click(object sender, EventArgs e)
        {

        }

        // Event handler to exit the application when label5 is clicked
        private void label5_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Event handler to minimize the window when label8 is clicked
        private void label8_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // Event handler for button1 click to navigate to Forms
        private async void button1_Click(object sender, EventArgs e)
        {
            Forms f = new Forms();
            f.Show();
            await Task.Delay(500);
            this.Hide();
        }

        // Event handler for button2 click to navigate to Category
        private async void button2_Click(object sender, EventArgs e)
        {
            Category c = new Category();
            c.Show();
            await Task.Delay(500);
            this.Hide();
        }

        // Event handler for button3 click to navigate to Attendants
        private async void button3_Click(object sender, EventArgs e)
        {
            Attendants a = new Attendants();
            a.Show();
            await Task.Delay(500);
            this.Hide();
        }

        // Constants and methods for enabling window dragging
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        // Event handler to enable dragging of the History window
        private void History_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        // Event handler for button4 click to navigate to LOGIN
        private void button4_Click(object sender, EventArgs e)
        {
            LOGIN lg = new LOGIN();
            lg.Show();
            this.Hide();
        }
    }
}

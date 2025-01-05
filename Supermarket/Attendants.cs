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
    public partial class Attendants : Form
    {
        public Attendants()
        {
            InitializeComponent(); // Initialize form components
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Close the application when the label is clicked
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Forms pd = new Forms(); // Open the Forms screen
            pd.Show();
            await Task.Delay(500); // Introduce a small delay
            this.Hide(); // Hide the current form
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Category ct = new Category(); // Open the Category screen
            ct.Show();
            await Task.Delay(500); // Introduce a small delay
            this.Hide(); // Hide the current form
        }

        // SQL connection string for connecting to the database
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-LBKQMDG;Initial Catalog=shopdb;Integrated Security=True;Encrypt=False;Connect Timeout=30");


        //Data Source=DESKTOP-LBKQMDG;Initial Catalog=shopdb;Integrated Security=True;Encrypt=False
        private void attaddbtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Check for missing information
                if (attname.Text == "" || dob.Text == "" || number.Text == "" || password.Text == "")
                {
                    MessageBox.Show("Can't Add !\t\n Missing Info"); // Display error message
                }
                else
                {
                    Con.Open(); // Open database connection
                    // Insert query to add a new attendant
                    String query = "insert into AttTable (AttName, Age, Number, Password) values ('" + attname.Text + "'," + dob.Text + "," + number.Text + ",'" + password.Text + "')";
                    SqlCommand command = new SqlCommand(query, Con);
                    command.ExecuteNonQuery(); // Execute the query
                    MessageBox.Show("Attendant Added Successfully"); // Confirm success
                    Con.Close(); // Close database connection
                    // Clear input fields
                    attid.Text = "";
                    attname.Text = "";
                    dob.Text = "";
                    number.Text = "";
                    password.Text = "";
                    fetchData(); // Refresh data
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Display exception message
                Con.Close(); // Ensure the connection is closed
            }
        }

        private void fetchData()
        {
            Con.Open(); // Open database connection
            // Fetch data query
            string query = "select * from AttTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var data = new DataSet();
            sda.Fill(data); // Fill data into dataset
            attList.DataSource = data.Tables[0]; // Bind data to DataGridView
            Con.Close(); // Close database connection
        }

        private void Attendants_Load(object sender, EventArgs e)
        {
            fetchData(); // Load data when form is loaded
        }

        private void attList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Populate input fields with selected row data
            attid.Text = attList.SelectedRows[0].Cells[0].Value.ToString();
            attname.Text = attList.SelectedRows[0].Cells[1].Value.ToString();
            dob.Text = attList.SelectedRows[0].Cells[2].Value.ToString();
            number.Text = attList.SelectedRows[0].Cells[3].Value.ToString();
            password.Text = attList.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void atteditbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (attid.Text == "")
                {
                    // Display error if no attendant is selected
                    MessageBox.Show("Attendant Not Selected \nPlease select the attendant to edit");
                }
                else
                {
                    Con.Open(); // Open database connection
                    // Update query for editing attendant details
                    String query = "update AttTable set AttName='" + attname.Text + "', Age=" + dob.Text + ", Number=" + number.Text + ", Password='" + password.Text + "'where AttID=" + attid.Text + ";";
                    SqlCommand command = new SqlCommand(query, Con);
                    command.ExecuteNonQuery(); // Execute the query
                    MessageBox.Show("Attendant Edited Successfully"); // Confirm success
                    Con.Close(); // Close database connection
                    // Clear input fields
                    attid.Text = "";
                    attname.Text = "";
                    dob.Text = "";
                    number.Text = "";
                    password.Text = "";
                    fetchData(); // Refresh data
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Display exception message
                Con.Close(); // Ensure the connection is closed
            }
        }

        private void attdelbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (attid.Text == "")
                {
                    // Display error if no attendant is selected
                    MessageBox.Show("Attendant Not Selected \nPlease select the Attendant to delete");
                }
                else
                {
                    Con.Open(); // Open database connection
                    // Delete query for removing an attendant
                    String query = "delete from AttTable where AttID=" + attid.Text + "";
                    SqlCommand command = new SqlCommand(query, Con);
                    command.ExecuteNonQuery(); // Execute the query
                    MessageBox.Show("Attendant Deleted Successfully"); // Confirm success
                    Con.Close(); // Close database connection
                    // Clear input fields
                    attid.Text = "";
                    attname.Text = "";
                    dob.Text = "";
                    number.Text = "";
                    password.Text = "";
                    fetchData(); // Refresh data
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Display exception message
                Con.Close(); // Ensure the connection is closed
            }
        }

        private void attid_TextChanged(object sender, EventArgs e)
        {
            // Placeholder for text changed event
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            History h = new History(); // Open the History screen
            h.Show();
            await Task.Delay(500); // Introduce a small delay
            this.Hide(); // Hide the current form
        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            Application.Exit(); // Close the application when the label is clicked
        }

        private void label8_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized; // Minimize the window
        }

        private void label8_MouseDown(object sender, MouseEventArgs e)
        {
            // Placeholder for mouse down event
        }

        // Constants and imports for dragging the form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void Attendants_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture(); // Release the capture of the mouse
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0); // Send message to move the window
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LOGIN lg = new LOGIN(); // Open the login screen
            lg.Show();
            this.Hide(); // Hide the current form
        }
    }
}

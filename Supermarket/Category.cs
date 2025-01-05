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
    public partial class Category : Form
    {
        public Category()
        {
            InitializeComponent(); // Initializes the components for the form.
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Closes the application when the label is clicked.
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            Attendants att = new Attendants(); // Creates an instance of the Attendants form.
            att.Show(); // Displays the Attendants form.
            await Task.Delay(500); // Delays for 500 milliseconds.
            this.Hide(); // Hides the current form.
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Forms pd = new Forms(); // Creates an instance of the Forms class.
            pd.Show(); // Displays the Forms.
            await Task.Delay(500); // Delays for 500 milliseconds.
            this.Hide(); // Hides the current form.
        }

        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-LBKQMDG;Initial Catalog=shopdb;Integrated Security=True;Encrypt=False;Connect Timeout=30");
        // Establishes a connection to the SQL Server database.

        //Data Source=DESKTOP-LBKQMDG;Initial Catalog=shopdb;Integrated Security=True;Encrypt=False

        private void cataddbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (catname.Text == "" || description.Text == "")
                {
                    MessageBox.Show("Can't Add !\t\n Missing Info"); // Prompts if required fields are empty.
                }
                else
                {
                    Con.Open(); // Opens the database connection.
                    String query = "insert into CatTable (CatName, Description) values ('" + catname.Text + "','" + description.Text + "')";
                    // SQL query to insert a new category.
                    SqlCommand command = new SqlCommand(query, Con); // Creates an SQL command.
                    command.ExecuteNonQuery(); // Executes the SQL command.
                    MessageBox.Show("Category Added Successfully"); // Displays a success message.
                    Con.Close(); // Closes the database connection.
                    catid.Text = ""; // Clears the CatID field.
                    catname.Text = ""; // Clears the CatName field.
                    description.Text = ""; // Clears the Description field.
                    fetchCat(); // Refreshes the category list.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Displays any errors encountered.
                Con.Close(); // Ensures the database connection is closed.
            }
        }

        private void fetchCat()
        {
            Con.Open(); // Opens the database connection.
            string query = "select * from CatTable"; // SQL query to fetch all categories.
            SqlDataAdapter sda = new SqlDataAdapter(query, Con); // Data adapter to execute the query.
            SqlCommandBuilder builder = new SqlCommandBuilder(sda); // Helps in managing the data adapter.
            var data = new DataSet(); // Creates a dataset to hold the data.
            sda.Fill(data); // Fills the dataset with the query result.
            catList.DataSource = data.Tables[0]; // Binds the dataset to the DataGridView.
            Con.Close(); // Closes the database connection.
        }

        private void Category_Load(object sender, EventArgs e)
        {
            fetchCat(); // Fetches and displays categories when the form loads.
        }

        private void catList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            catid.Text = catList.SelectedRows[0].Cells[0].Value.ToString(); // Populates CatID from the selected row.
            catname.Text = catList.SelectedRows[0].Cells[1].Value.ToString(); // Populates CatName from the selected row.
            description.Text = catList.SelectedRows[0].Cells[2].Value.ToString(); // Populates Description from the selected row.
        }

        private void catdelbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (catid.Text == "")
                {
                    MessageBox.Show("Product Not Selected \nPlease select the product to delete"); // Prompts if no product is selected.
                }
                else
                {
                    Con.Open(); // Opens the database connection.
                    String query = "delete from CatTable where CatID=" + catid.Text + ""; // SQL query to delete a category.
                    SqlCommand command = new SqlCommand(query, Con); // Creates an SQL command.
                    command.ExecuteNonQuery(); // Executes the SQL command.
                    MessageBox.Show("Category Deleted Successfully"); // Displays a success message.
                    Con.Close(); // Closes the database connection.
                    catid.Text = ""; // Clears the CatID field.
                    catname.Text = ""; // Clears the CatName field.
                    description.Text = ""; // Clears the Description field.
                    fetchCat(); // Refreshes the category list.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Displays any errors encountered.
                Con.Close(); // Ensures the database connection is closed.
            }
        }

        private void cateditbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (catid.Text == "")
                {
                    MessageBox.Show("Category Not Selected \nPlease select the category to edit"); // Prompts if no category is selected.
                }
                else
                {
                    Con.Open(); // Opens the database connection.
                    String query = "update CatTable set CatName='" + catname.Text + "', Description='" + description.Text + "'where CatID=" + catid.Text + ";";
                    // SQL query to update a category.
                    SqlCommand command = new SqlCommand(query, Con); // Creates an SQL command.
                    command.ExecuteNonQuery(); // Executes the SQL command.
                    MessageBox.Show("Category Edited Successfully"); // Displays a success message.
                    Con.Close(); // Closes the database connection.
                    catid.Text = ""; // Clears the CatID field.
                    catname.Text = ""; // Clears the CatName field.
                    description.Text = ""; // Clears the Description field.
                    fetchCat(); // Refreshes the category list.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Displays any errors encountered.
                Con.Close(); // Ensures the database connection is closed.
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            History h = new History(); // Creates an instance of the History form.
            h.Show(); // Displays the History form.
            await Task.Delay(500); // Delays for 500 milliseconds.
            this.Hide(); // Hides the current form.
        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            Application.Exit(); // Closes the application.
        }

        private void label8_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized; // Minimizes the application window.
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void Category_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture(); // Releases the mouse capture.
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0); // Sends a message to move the form.
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LOGIN lg = new LOGIN(); // Creates an instance of the LOGIN form.
            lg.Show(); // Displays the LOGIN form.
            this.Hide(); // Hides the current form.
        }
    }
}

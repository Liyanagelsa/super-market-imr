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
    public partial class Forms : Form
    {
        public Forms()
        {
            InitializeComponent(); // Initializes the form components.
        }

        // Fetches categories from the database and binds them to the category dropdown.
        private void FetchCat()
        {
            Con.Open(); // Opens the SQL connection.
            String query = "select CatName from CatTable"; // SQL query to fetch category names.
            SqlCommand command = new SqlCommand(query, Con); // Command to execute query.
            SqlDataReader read; // Reads data from the database.
            read = command.ExecuteReader();
            DataTable data = new DataTable(); // Creates a data table.
            data.Columns.Add("CatName", typeof(string)); // Adds a column for category names.
            data.Load(read); // Loads data from the reader into the table.
            category.ValueMember = "catName"; // Sets the value member for dropdown.
            category.DataSource = data; // Binds data to dropdown.
            Con.Close(); // Closes the SQL connection.
        }

        // Fetches categories for another dropdown (categoryS).
        private void FetchCat2()
        {
            Con.Open();
            String query = "select CatName from CatTable";
            SqlCommand command = new SqlCommand(query, Con);
            SqlDataReader read;
            read = command.ExecuteReader();
            DataTable data = new DataTable();
            data.Columns.Add("CatName", typeof(string));
            data.Load(read);
            categoryS.ValueMember = "catName";
            categoryS.DataSource = data;
            Con.Close();
        }

        // Loads data when the form is opened.
        private void Forms_Load(object sender, EventArgs e)
        {
            FetchCat(); // Loads category dropdown.
            FetchCat2(); // Loads second category dropdown.
            fetchData(); // Fetches product data.
        }

        // Exits the application when the label is clicked.
        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Navigates to the Category form and hides the current form.
        private async void button2_Click(object sender, EventArgs e)
        {
            Category ct = new Category();
            ct.Show();
            await Task.Delay(500); // Waits before hiding the current form.
            this.Hide();
        }

        // Navigates to the Attendants form and hides the current form.
        private async void button3_Click(object sender, EventArgs e)
        {
            Attendants att = new Attendants();
            att.Show();
            await Task.Delay(500);
            this.Hide();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-LBKQMDG;Initial Catalog=shopdb;Integrated Security=True;Encrypt=False;Connect Timeout=30"); // SQL connection string.

        // Adds a product to the database.
        private void prodaddbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (prodname.Text == "" || quantity.Text == "" || price.Text == "" || category.Text == "")
                {
                    MessageBox.Show("Can't Add !\t\n Missing Info"); // Checks for missing input.
                }
                else
                {
                    Con.Open();
                    String query = "insert into ProdTable (ProdName, Quantity, Price, Category) values ('" + prodname.Text + "'," + quantity.Text + "," + price.Text + ",'" + category.Text + "')";
                    SqlCommand command = new SqlCommand(query, Con);
                    command.ExecuteNonQuery(); // Executes the query.
                    MessageBox.Show("Product Added Successfully");
                    Con.Close();
                    prodid.Text = "";
                    prodname.Text = "";
                    quantity.Text = "";
                    price.Text = "";
                    tid.Text = "";
                    tqty.Text = "";
                    fetchData(); // Refreshes the product data.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Shows error message.
                Con.Close();
            }
        }

        // Fetches all product data and binds it to the grid.
        private void fetchData()
        {
            Con.Open();
            string query = "select * from ProdTable"; // SQL query to fetch all products.
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var data = new DataSet();
            sda.Fill(data); // Fills the dataset with product data.
            prodList.DataSource = data.Tables[0]; // Binds data to grid.
            Con.Close();
        }

        // Fetches product data specific to a selected category.
        private void fetchDataSpecific()
        {
            Con.Open();
            string query = "select * from ProdTable where Category='" + categoryS.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var data = new DataSet();
            sda.Fill(data);
            prodList.DataSource = data.Tables[0];
            Con.Close();
        }

        // Fills form fields with data from the selected product in the grid.
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            prodid.Text = prodList.SelectedRows[0].Cells[0].Value.ToString();
            prodname.Text = prodList.SelectedRows[0].Cells[1].Value.ToString();
            quantity.Text = prodList.SelectedRows[0].Cells[2].Value.ToString();
            price.Text = prodList.SelectedRows[0].Cells[3].Value.ToString();
            category.Text = prodList.SelectedRows[0].Cells[4].Value.ToString();
            tid.Text = prodList.SelectedRows[0].Cells[0].Value.ToString();
        }

        // Edits a selected product in the database.
        private void prodeditbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (prodid.Text == "")
                {
                    MessageBox.Show("Product Not Selected \nPlease select the product to edit"); // Checks if a product is selected.
                }
                else
                {
                    Con.Open();
                    String query = "update ProdTable set ProdName='" + prodname.Text + "', Quantity=" + quantity.Text + ", Price=" + price.Text + ", Category='" + category.Text + "' where ProdID=" + prodid.Text + ";";
                    SqlCommand command = new SqlCommand(query, Con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Product Edited Successfully");
                    Con.Close();
                    prodid.Text = "";
                    prodname.Text = "";
                    quantity.Text = "";
                    price.Text = "";
                    tid.Text = "";
                    tqty.Text = "";
                    fetchData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Con.Close();
            }
        }

        // Deletes a selected product from the database.
        private void proddelbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (prodid.Text == "")
                {
                    MessageBox.Show("Product Not Selected \nPlease select the Product to delete");
                }
                else
                {
                    Con.Open();
                    String query = "delete from ProdTable where ProdID=" + prodid.Text + "";
                    SqlCommand command = new SqlCommand(query, Con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted Successfully");
                    Con.Close();
                    prodid.Text = "";
                    prodname.Text = "";
                    quantity.Text = "";
                    price.Text = "";
                    tid.Text = "";
                    tqty.Text = "";
                    fetchData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Con.Close();
            }
        }

        // Handles dropdown category selection change to filter products.
        private void categoryS_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void categoryS_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchDataSpecific();
        }

        // Refreshes the product data.
        private void refresh_Click(object sender, EventArgs e)
        {
            fetchData();
        }

        // Navigates to the History form and hides the current form.
        private async void button1_Click(object sender, EventArgs e)
        {
            History h = new History();
            h.Show();
            await Task.Delay(500);
            this.Hide();
        }

        // Exits the application when another label is clicked.
        private void label5_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Minimizes the form when the label is clicked.
        private void label8_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // Constants and methods for allowing the form to be dragged by clicking anywhere.
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void Forms_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        // Fetches products based on text search.
        private void fetchDataSpecificText()
        {
            Con.Open();
            string query = "select * from ProdTable where ProdName like '" + "%" + search.Text + "%" + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var data = new DataSet();
            sda.Fill(data);
            prodList.DataSource = data.Tables[0];
            Con.Close();
        }

        // Updates product data as the user types in the search box.
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            fetchDataSpecificText();
        }

        // Increases the quantity of a selected product.
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (tqty.Text == "" || tid.Text == "")
                {
                    MessageBox.Show("Missing Info");
                }
                else
                {
                    Con.Open();
                    String query = "update ProdTable set Quantity = Quantity+" + Convert.ToInt32(tqty.Text) + " where ProdID=" + tid.Text + ";";
                    SqlCommand command = new SqlCommand(query, Con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Product Quantity Increased Successfully");
                    Con.Close();
                    prodid.Text = "";
                    prodname.Text = "";
                    quantity.Text = "";
                    price.Text = "";
                    tid.Text = "";
                    tqty.Text = "";
                    fetchData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Con.Close();
            }
        }

        // Navigates to the login form.
        private void button4_Click(object sender, EventArgs e)
        {
            LOGIN lg = new LOGIN();
            lg.Show();
            this.Hide();
        }
    }
}
